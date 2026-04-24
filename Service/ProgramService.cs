using Microsoft.Data.SqlClient;
using NIAUNIVERSITYPANELAPI.Models;
using System.Data;

namespace NIAUNIVERSITYPANELAPI.Service
{
    public class ProgramService
    {
        private readonly string _connectionString;

        public ProgramService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ProgramModel> GetPrograms()
        {
            List<ProgramModel> list = new List<ProgramModel>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetProgramList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new ProgramModel
                    {
                        programId = Convert.ToInt32(dr["program_Id"]),
                        ProgramName = dr["program_name"].ToString(),
                        IsActive = Convert.ToBoolean(dr["Is_active"])
                    });
                }
            }
            return list;
        }
       
        public List<ExamModel> GetExamList()
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
                        exam_id = Convert.ToInt32(dr["exam_id"]),
                        course_name = dr["course_name"].ToString()!,
                        exam_name = dr["exam_name"].ToString()!                        
                    });
                }
            }
            return list;
        }

        public void AddProgram(ProModel model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddProgram", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgramName", model.programName);
                cmd.Parameters.AddWithValue("@IsActive", model.isActive);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateProgram(ProModel model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateProgram", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgramId", model.programId);
                cmd.Parameters.AddWithValue("@ProgramName", model.programName);
                cmd.Parameters.AddWithValue("@IsActive", model.isActive);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool DeleteProgram(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteProgram", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgramId", id);
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public bool DeleteCourse(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteCourse", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CourseId", id);
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public List<Course> GetCoursest()
        {
            List<Course> list = new();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetCourses", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new Course
                    {
                        Course_Id = Convert.ToInt32(dr["Course_Id"]),
                        ProgramId = Convert.ToInt32(dr["program_Id"]),
                        ProgramName = dr["program_name"].ToString()!,
                        Course_Name = dr["Course_Name"].ToString()!,
                        IsActive = Convert.ToBoolean(dr["Is_Active"])
                    });
                }
            }

            return list;
        }
        public void AddCourse(Course model)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_AddCourse", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProgramId", model.ProgramId);
            cmd.Parameters.AddWithValue("@CourseName", model.Course_Name);
            cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateCourse(Course model)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_UpdateCourse", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Course_Id", model.Course_Id);
            cmd.Parameters.AddWithValue("@ProgramId", model.ProgramId);
            cmd.Parameters.AddWithValue("@CourseName", model.Course_Name);
            cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
            con.Open();
            cmd.ExecuteNonQuery();
        }

    }
}
