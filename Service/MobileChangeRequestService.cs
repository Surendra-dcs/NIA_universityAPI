using Microsoft.Data.SqlClient;
using NIAUNIVERSITYPANELAPI.Models;
using System.Data;

namespace NIAUNIVERSITYPANELAPI.Service
{
    public class MobileChangeRequestService
    {
        private readonly string _connectionString;

        public MobileChangeRequestService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // ── GET: all requests, optionally filtered by status ─────────────────
        public List<MobileChangeRequestModel> GetAll(string? status = null)
        {
            var list = new List<MobileChangeRequestModel>();

            using SqlConnection con = new SqlConnection(_connectionString);

            // Join tbl_MobileChangeRequest → Users (for name)
            //                              → tbl_StudentExamRollMaster (for roll number)
            string query = @"
                SELECT
                    mcr.Id,
                    mcr.UserId,
                    u.FullName          AS StudentName,
                    ISNULL(r.RollNumber, '')  AS RollNumber,
                    mcr.OldMobile,
                    mcr.NewMobile,
                    mcr.Status,
                    CONVERT(VARCHAR(16), mcr.CreatedAt, 120) AS CreatedAt
                FROM tbl_MobileChangeRequest mcr
                INNER JOIN Users u
                    ON mcr.UserId = u.Id
                LEFT JOIN tbl_StudentExamRollMaster r
                    ON r.UserId = mcr.UserId
                    AND r.Status = 'Submitted'
                WHERE (@Status IS NULL OR mcr.Status = @Status)
                -- deduplicate if a student has multiple rolls; take the latest
                ORDER BY mcr.CreatedAt DESC";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Status",
                string.IsNullOrWhiteSpace(status) ? (object)DBNull.Value : status.Trim());

            con.Open();
            using SqlDataReader dr = cmd.ExecuteReader();

            // Track seen IDs to avoid duplicates from the LEFT JOIN
            var seen = new HashSet<int>();

            while (dr.Read())
            {
                int id = Convert.ToInt32(dr["Id"]);
                if (seen.Contains(id)) continue;   // skip duplicate rows from multi-roll join
                seen.Add(id);

                list.Add(new MobileChangeRequestModel
                {
                    Id = id,
                    UserId = Convert.ToInt32(dr["UserId"]),
                    StudentName = dr["StudentName"]?.ToString() ?? "",
                    RollNumber = dr["RollNumber"]?.ToString() ?? "",
                    OldMobile = dr["OldMobile"]?.ToString() ?? "",
                    NewMobile = dr["NewMobile"]?.ToString() ?? "",
                    Status = dr["Status"]?.ToString() ?? "",
                    CreatedAt = dr["CreatedAt"]?.ToString() ?? ""
                });
            }

            return list;
        }

        // ── POST: approve — update mobile in Users + PersonalDetails ─────────
        public (bool success, string message) Approve(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            // 1. Fetch the request
            string selectSql = @"
                SELECT Id, UserId, OldMobile, NewMobile, Status
                FROM tbl_MobileChangeRequest
                WHERE Id = @Id";

            string? oldMobile = null, newMobile = null, currentStatus = null;
            int userId = 0;

            using (SqlCommand sel = new SqlCommand(selectSql, con))
            {
                sel.Parameters.AddWithValue("@Id", id);
                using var dr = sel.ExecuteReader();
                if (!dr.Read())
                    return (false, "Request not found.");

                currentStatus = dr["Status"]?.ToString();
                if (currentStatus != "Pending")
                    return (false, $"Request is already {currentStatus}.");

                userId = Convert.ToInt32(dr["UserId"]);
                oldMobile = dr["OldMobile"]?.ToString();
                newMobile = dr["NewMobile"]?.ToString();
            }

            // 2. Check new mobile not already taken by another user
            using (SqlCommand chk = new SqlCommand(
                "SELECT COUNT(1) FROM Users WHERE Mobile = @Mobile AND Id <> @UserId", con))
            {
                chk.Parameters.AddWithValue("@Mobile", newMobile);
                chk.Parameters.AddWithValue("@UserId", userId);
                int taken = (int)chk.ExecuteScalar();
                if (taken > 0)
                    return (false, "The requested mobile number is already registered to another account.");
            }

            // 3. Update Users.Mobile
            using (SqlCommand updUser = new SqlCommand(
                "UPDATE Users SET Mobile = @NewMobile WHERE Id = @UserId", con))
            {
                updUser.Parameters.AddWithValue("@NewMobile", newMobile);
                updUser.Parameters.AddWithValue("@UserId", userId);
                updUser.ExecuteNonQuery();
            }

            // 4. Update PersonalDetails.Phone (via FormApplications join)
            using (SqlCommand updPd = new SqlCommand(@"
                UPDATE pd SET pd.Phone = @NewMobile
                FROM PersonalDetails pd
                INNER JOIN FormApplications fa ON fa.Id = pd.ApplicationId
                WHERE fa.UserId = @UserId", con))
            {
                updPd.Parameters.AddWithValue("@NewMobile", newMobile);
                updPd.Parameters.AddWithValue("@UserId", userId);
                updPd.ExecuteNonQuery();
            }

            // 5. Mark the request as Approved
            using (SqlCommand updReq = new SqlCommand(@"
                UPDATE tbl_MobileChangeRequest
                SET Status = 'Approved', UpdatedAt = GETUTCDATE()
                WHERE Id = @Id", con))
            {
                updReq.Parameters.AddWithValue("@Id", id);
                updReq.ExecuteNonQuery();
            }

            return (true, "Mobile number updated successfully.");
        }

        // ── POST: reject ──────────────────────────────────────────────────────
        public (bool success, string message) Reject(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            // Verify it exists and is still Pending
            string? currentStatus = null;
            using (SqlCommand sel = new SqlCommand(
                "SELECT Status FROM tbl_MobileChangeRequest WHERE Id = @Id", con))
            {
                sel.Parameters.AddWithValue("@Id", id);
                currentStatus = sel.ExecuteScalar()?.ToString();
            }

            if (currentStatus == null)
                return (false, "Request not found.");
            if (currentStatus != "Pending")
                return (false, $"Request is already {currentStatus}.");

            using SqlCommand upd = new SqlCommand(@"
                UPDATE tbl_MobileChangeRequest
                SET Status = 'Rejected', UpdatedAt = GETUTCDATE()
                WHERE Id = @Id", con);
            upd.Parameters.AddWithValue("@Id", id);
            upd.ExecuteNonQuery();

            return (true, "Request rejected.");
        }
    }
}