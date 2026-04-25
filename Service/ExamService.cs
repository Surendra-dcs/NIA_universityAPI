using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NIAUNIVERSITYPANELAPI.Models;
using System.Data;

namespace NIAUNIVERSITYPANELAPI.Service
{
    public class ExamService : IExamService
    {
        private readonly string _connectionString;

        public ExamService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public string SaveExam(ExamModel model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SaveExam", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", model.ExamId);
                cmd.Parameters.AddWithValue("@CourseId", model.CourseId);
                cmd.Parameters.AddWithValue("@programId", model.ProgramId);
                cmd.Parameters.AddWithValue("@ExamName", model.ExamName);
                cmd.Parameters.AddWithValue("@Status", model.Status);
                con.Open();
                return cmd.ExecuteScalar()?.ToString()!;
            }
        }

        public List<ExamModel> GetExams()
        {
            List<ExamModel> list = new List<ExamModel>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetExamList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new ExamModel
                    {
                        ExamId = Convert.ToInt32(dr["exam_id"]),
                        CourseId = Convert.ToInt32(dr["course_id"]),
                        ProgramId = Convert.ToInt32(dr["program_Id"]),
                        CourseName = dr["course_name"].ToString()!,
                        ProgramName = dr["program_name"].ToString()!,
                        ExamName = dr["exam_name"].ToString()!,
                        Status = Convert.ToBoolean(dr["is_active"])
                    });
                }
            }
            return list;
        }

        public ExamModel GetExamById(int id)
        {
            ExamModel model = new ExamModel();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetExamById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    model.ExamId = Convert.ToInt32(dr["exam_id"]);
                    model.CourseId = Convert.ToInt32(dr["course_id"]);
                    model.ProgramId = Convert.ToInt32(dr["program_Id"]);
                    model.ExamName = dr["exam_name"].ToString()!;
                    model.Status = Convert.ToBoolean(dr["is_active"]);
                }
            }
            return model;
        }

        public string DeleteExam(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteExam", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return "Deleted Successfully";
        }



        public List<Rolllist> GetRolllist()
        {
            List<Rolllist> list = new List<Rolllist>();

            using SqlConnection conn = new SqlConnection(_connectionString);

            string query = "SELECT sem.RollNumber,sem.EnrollmentNumber,sem.SemesterId,sy.semyearname,sem.CourseId,cm.course_name,cm.Examname,sem.UserId,u.FullName,u.Email FROM tbl_StudentExamRollMaster sem INNER JOIN tbl_SemesterYear sy ON sem.SemesterId = sy.semId INNER JOIN tbl_CourseMaster cm ON sem.CourseId = cm.course_Id INNER JOIN Users u ON sem.UserId = u.id";

            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Rolllist
                {
                    UserId = Convert.ToInt32(reader["UserId"]),
                    CourseName = reader["course_name"]?.ToString(),
                    SemesterName = reader["semyearname"]?.ToString(),
                    ExamName = reader["Examname"]?.ToString(),
                    RollNumber = reader["RollNumber"]?.ToString(),
                    UserName = reader["FullName"]?.ToString(),
                    Email = reader["Email"]?.ToString(),
                });
            }

            return list;
        }

        
             public List<Resultlist> Getresultlist()
        {
            List<Resultlist> list = new List<Resultlist>();

            using SqlConnection conn = new SqlConnection(_connectionString);

            string query = "SELECT  erm.roll_number, erm.exam_id, em.exam_name,erm.attempt,  erm.result_status, erm.falil_in_paper, sem.UserId, u.FullName, u.Email FROM tbl_ExamResultMaster erm INNER JOIN tbl_StudentExamRollMaster sem ON erm.roll_number = sem.RollNumber INNER JOIN tbl_ExamMaster em  ON erm.exam_id = em.exam_id INNER JOIN Users u  ON sem.UserId = u.Id;";

            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Resultlist
                {

                    resultStatus = reader["result_status"]?.ToString(),
                    attempt = reader["attempt"]?.ToString(),
                    fallinpaper = reader["falil_in_paper"]?.ToString(),
                    ExamName = reader["exam_name"]?.ToString(),
                    RollNumber = reader["roll_number"]?.ToString(),
                    UserName = reader["FullName"]?.ToString(),
                    Email = reader["Email"]?.ToString(),
                });
            }

            return list;
        }
    }
}
