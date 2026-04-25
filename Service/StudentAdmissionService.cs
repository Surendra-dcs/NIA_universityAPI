using Microsoft.Data.SqlClient;
using NIAUNIVERSITYPANELAPI.Models;
using System.Data;

namespace NIAUNIVERSITYPANELAPI.Service
{
    public class StudentAdmissionService
    {
        private readonly string _connectionString;

        public StudentAdmissionService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<StudentAdmissionModel> GetStudentAdmissionList()
        {
            List<StudentAdmissionModel> list = new List<StudentAdmissionModel>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_StudentAdmissionList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new StudentAdmissionModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        FormNumber = dr["FormNumber"].ToString(),
                        StudentName = dr["StudentName"].ToString(),
                        FatherName = dr["FatherName"].ToString(),
                        MotherName = dr["MotherName"].ToString(),
                        Mobile = dr["Mobile"].ToString(),
                        Email = dr["Email"].ToString(),
                        Program = dr["Name"].ToString(),
                        Course = dr["course_name"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        DateOfBirth = dr["DateOfBirth"].ToString()
                    });
                }
            }
            return list;
        }

        public StudentAdmissionModel GetStudentByMobile(string mobile)
        {
            StudentAdmissionModel student = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetStudentByMobile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mobile", mobile);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    student = new StudentAdmissionModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FormNumber = dr["FormNumber"].ToString(),
                        StudentName = dr["StudentName"].ToString(),
                        FatherName = dr["FatherName"].ToString(),
                        MotherName = dr["MotherName"].ToString(),
                        Mobile = dr["Mobile"].ToString(),
                        Email = dr["Email"].ToString(),
                        Program = dr["Program"].ToString(),
                        Course = dr["Course"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        DateOfBirth = dr["DateOfBirth"].ToString()
                    };
                }
            }

            return student;
        }

        public bool UpdateStudent(StudentAdmissionModel model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateStudentDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@StudentName", model.StudentName);
                cmd.Parameters.AddWithValue("@FatherName", model.FatherName);
                cmd.Parameters.AddWithValue("@MotherName", model.MotherName);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Mobile", model.Mobile);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                return rows > 0;
            }
        }

    }
}
