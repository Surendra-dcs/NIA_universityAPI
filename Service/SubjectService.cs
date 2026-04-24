using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NIAUNIVERSITYPANELAPI.Models;
using System.Data;

namespace NIAUNIVERSITYPANELAPI.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly string _connectionString;

        public SubjectService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public string SaveSubject(SubjectModel model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SaveSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubjectId", model.SubjectId);
                cmd.Parameters.AddWithValue("@ProgramId", model.ProgramId);
                cmd.Parameters.AddWithValue("@CourseId", model.CourseId);
                cmd.Parameters.AddWithValue("@SubjectName", model.SubjectName);
                cmd.Parameters.AddWithValue("@SubjectCode", model.SubjectCode);
                cmd.Parameters.AddWithValue("@Status", model.Status);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return "Saved Successfully";
        }

        public List<SubjectModel> GetSubjects()
        {
            List<SubjectModel> list = new List<SubjectModel>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetSubjects", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new SubjectModel
                    {
                        SubjectId = Convert.ToInt32(dr["subject_id"]),
                        ProgramId = Convert.ToInt32(dr["ProgramId"]),
                        CourseId = Convert.ToInt32(dr["course_id"]),
                        SubjectName = dr["subject_name"].ToString()!,
                        programName = dr["program_name"].ToString()!,
                        courseName = dr["Course_Name"].ToString()!,
                        SubjectCode = dr["subject_code"].ToString()!,
                        SemPart = dr["SemYearCode"].ToString()!,
                        Status = Convert.ToBoolean(dr["Is_active"])
                    });
                }
            }

            return list;
        }

        public List<Course> GetCoursesByProgram(int programId)
        {
            List<Course> list = new List<Course>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetCourseByProgram", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgramId", programId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new Course
                    {
                        Course_Id = Convert.ToInt32(dr["course_Id"]),
                        Course_Name = dr["Course_Name"].ToString()!
                    });
                }
            }

            return list;
        }
        public SubjectModel GetSubjectById(int id)
        {
            SubjectModel model = new SubjectModel();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetSubjectById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubjectId", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    model.SubjectId = Convert.ToInt32(dr["subject_id"]);
                    model.ProgramId = Convert.ToInt32(dr["ProgramId"]);
                    model.CourseId = Convert.ToInt32(dr["course_id"]);
                    model.SubjectName = dr["subject_name"].ToString()!;
                    model.SubjectCode = dr["subject_code"].ToString()!;
                    model.Status = Convert.ToBoolean(dr["Is_Active"]);
                }
            }
            return model;
        }
        public string DeleteSubject(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubjectId", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return "Deleted Successfully";
        }
    }
}
