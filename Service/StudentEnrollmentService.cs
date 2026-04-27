using Microsoft.Data.SqlClient;
using NIAUNIVERSITYPANELAPI.Models;
using System.Data;

namespace NIAUNIVERSITYPANELAPI.Service
{
    public class StudentEnrollmentService
    {
        private readonly string _connectionString;

        public StudentEnrollmentService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<StudentEnrollmentModel> GetStudentEnrollmentList()
        {
            List<StudentEnrollmentModel> list = new List<StudentEnrollmentModel>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_StudentEnrollmentList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new StudentEnrollmentModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        UserId = Convert.ToInt32(dr["UserId"]),
                        EnrollmentNumber = dr["EnrollmentNumber"].ToString(),
                        CollegeName = dr["CollegeName"].ToString(),
                        ProgramName = dr["ProgramName"].ToString(),
                        CourseName = dr["CourseName"].ToString(),
                        StudentName = dr["StudentName"].ToString(),
                        FatherName = dr["FatherName"].ToString(),
                        MotherName = dr["MotherName"].ToString(),
                        Email = dr["Email"].ToString(),
                        Mobile = dr["Mobile"].ToString(),
                        IsVerify = dr["IsVerify"] != DBNull.Value && Convert.ToBoolean(dr["IsVerify"]),

                        // ── Rich fields (were missing) ──
                        FormNumber = dr["FormNumber"] != DBNull.Value ? dr["FormNumber"].ToString() : "",
                        RollNumber = dr["RollNumber"] != DBNull.Value ? dr["RollNumber"].ToString() : "",
                        AadhaarNumber = dr["AadhaarNumber"] != DBNull.Value ? dr["AadhaarNumber"].ToString() : "",
                        AbcId = dr["AbcId"] != DBNull.Value ? dr["AbcId"].ToString() : "",
                        Attempt = dr["Attempt"] != DBNull.Value ? dr["Attempt"].ToString() : "",
                        StudentNameHindi = dr["StudentNameHindi"] != DBNull.Value ? dr["StudentNameHindi"].ToString() : "",
                        FatherNameHindi = dr["FatherNameHindi"] != DBNull.Value ? dr["FatherNameHindi"].ToString() : "",
                        MotherNameHindi = dr["MotherNameHindi"] != DBNull.Value ? dr["MotherNameHindi"].ToString() : "",
                        Gender = dr["Gender"] != DBNull.Value ? dr["Gender"].ToString() : "",
                        DateOfBirth = dr["DateOfBirth"] != DBNull.Value ? dr["DateOfBirth"].ToString() : "",
                        Category = dr["Category"] != DBNull.Value ? dr["Category"].ToString() : "",
                        PwdCategory = dr["PwdCategory"] != DBNull.Value ? dr["PwdCategory"].ToString() : "",
                        Religion = dr["Religion"] != DBNull.Value ? dr["Religion"].ToString() : "",
                        State = dr["State"] != DBNull.Value ? dr["State"].ToString() : "",
                        District = dr["District"] != DBNull.Value ? dr["District"].ToString() : "",
                        CandidateImagePath = dr["CandidateImagePath"] != DBNull.Value ? dr["CandidateImagePath"].ToString() : "",
                        SignatureImagePath = dr["SignatureImagePath"] != DBNull.Value ? dr["SignatureImagePath"].ToString() : "",
                    });
                }
            }
            return list;
        }

        public bool VerifyStudent(StudentEnrollmentModel model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_VerifyStudent", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", model.Id);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                return rows > 0;
            }
        }

    }
}