using Microsoft.Data.SqlClient;
using NIAUNIVERSITYPANELAPI.Models;
using System.Data;

namespace NIAUNIVERSITYPANELAPI.Service
{
    public class StudentCorrectionService
    {
        private readonly string _connectionString;

        public StudentCorrectionService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public StudentCorrectionModel GetStudentByMobile(string mobile)
        {
            StudentCorrectionModel student = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetStudentByMobile", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = mobile;

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        // ── Helper: safely read a column that may not exist in older SP versions ──
                        string SafeRead(string col)
                        {
                            try
                            {
                                int ord = dr.GetOrdinal(col);
                                return dr.IsDBNull(ord) ? "" : dr.GetString(ord);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                return "";
                            }
                        }

                        student = new StudentCorrectionModel
                        {
                            Id = dr["Id"] != DBNull.Value ? Convert.ToInt32(dr["Id"]) : 0,
                            userId = dr["userId"] != DBNull.Value ? Convert.ToInt32(dr["userId"]) : 0,
                            StudentName = dr["FirstName"]?.ToString(),
                            FatherName = dr["FatherName"]?.ToString(),
                            MotherName = dr["MotherName"]?.ToString(),
                            AlternateMobile = dr["AlternateMobile"]?.ToString(),
                            DateOfBirth = dr["DateOfBirth"] != DBNull.Value
                                                ? Convert.ToDateTime(dr["DateOfBirth"])
                                                : (DateTime?)null,
                            Address = dr["AddressLine1"]?.ToString(),
                            MainMobile = dr["mainMobile"]?.ToString(),

                            // ── S3 image paths ────────────────────────────────────────────────
                            CandidateImagePath = SafeRead("CandidateImagePath"),
                            SignatureImagePath = SafeRead("SignatureImagePath"),
                        };
                    }
                }
            }

            return student;
        }

        public bool UpdateStudent(StudentCorrectionModel model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateStudentDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@StudentName", model.StudentName);
                cmd.Parameters.AddWithValue("@FatherName", model.FatherName);
                cmd.Parameters.AddWithValue("@MotherName", model.MotherName);
                cmd.Parameters.AddWithValue("@DateOfBirth", (object)model.DateOfBirth ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", model.Address ?? "");

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                return rows > 0;
            }
        }

        public bool UpdateMobile(UpdateMobileModel model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_UpdateMobileBothTables", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@UserId", model.userId);
                cmd.Parameters.AddWithValue("@Mobile", model.Mobile);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                return rows > 0;
            }
        }
    }
}