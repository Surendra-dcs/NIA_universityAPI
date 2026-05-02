using Microsoft.Data.SqlClient;
using NIAUNIVERSITYPANELAPI.Models;
using System.Data;

namespace NIAUNIVERSITYPANELAPI.Service
{
    public class RemunerationService
    {
        private readonly IConfiguration _config;

        public RemunerationService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<RemunerationVM>> GetBillAsync()
        {
            List<RemunerationVM> list = new List<RemunerationVM>();

            string connStr = _config.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                SELECT 
                    u.name,
                    u.email,
                    u.mobile,
                    u.PanNo,
                    u.BankName,
                    u.BankAccount,
                    u.IFSCCode,
                    qp.courseId,
                    cp.rates,
                    cm.course_name,
                    cp.rates AS TotalAmount
                FROM qp_task qp
            INNER JOIN tbl_qp_UsersMaster u ON u.id = qp.setter
                INNER JOIN tbl_CoursePayAmount cp ON qp.courseId = cp.courseId
                LEFT JOIN tbl_CourseMaster cm ON cm.course_Id = qp.courseId
                WHERE 
                    cp.workActivities = 'QuestionPaper'
                    AND qp.setter_status = 5
                    AND qp.moderator_status = 5

                UNION ALL

                SELECT 
                    u.name,
                    u.email,
                    u.mobile,
                    u.PanNo,
                    u.BankName,
                    u.BankAccount,
                    u.IFSCCode,
                    qp.courseId,
                    cp.rates,
                    cm.course_name,
                    cp.rates AS TotalAmount
                FROM qp_task qp
            INNER JOIN tbl_qp_UsersMaster u ON u.id = qp.moderator
                INNER JOIN tbl_CoursePayAmount cp ON qp.courseId = cp.courseId
                LEFT JOIN tbl_CourseMaster cm ON cm.course_Id = qp.courseId
                WHERE 
                    cp.workActivities = 'QuestionPaper'
                    AND qp.setter_status = 5
                    AND qp.moderator_status = 5

            ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    await con.OpenAsync();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        list.Add(new RemunerationVM
                        {
                            Name = reader["name"].ToString(),
                            Email = reader["email"].ToString(),
                            Mobile = reader["mobile"].ToString(),
                            PanNo = reader["PanNo"].ToString(),
                            BankName = reader["BankName"].ToString(),
                            BankAccount = reader["BankAccount"].ToString(),
                            IFSCCode = reader["IFSCCode"].ToString(),
                            CourseName = reader["course_name"].ToString(),
                            CourseId = Convert.ToInt32(reader["courseId"]),
                            Count = 1,
                            Rate = Convert.ToDecimal(reader["rates"]),
                            Amount = Convert.ToDecimal(reader["TotalAmount"])
                        });
                    }
                }
            }

            return list;
        }

    }
}