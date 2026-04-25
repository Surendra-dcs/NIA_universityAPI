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
    }
}
