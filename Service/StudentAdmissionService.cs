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
                        CollegeName = dr["CollegeName"].ToString(),
                        ProgramName = dr["ProgramName"].ToString(),
                        CourseName = dr["CourseName"].ToString(),
                        StudentName = dr["StudentName"].ToString(),
                        FatherName = dr["FatherName"].ToString(),
                        MotherName = dr["MotherName"].ToString(),
                        Mobile = dr["Mobile"].ToString(),
                        Email = dr["Email"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        DateOfBirth = dr["DateOfBirth"].ToString(),
                        Category = dr["Category"].ToString(),
                        Religion = dr["Religion"].ToString(),
                        Nationality = dr["Nationality"].ToString(),
                        AadhaarId = dr["AadhaarId"].ToString(),
                        AbcId = dr["AbcId"].ToString(),
                        AddressLine1 = dr["AddressLine1"].ToString(),
                        District = dr["District"].ToString(),
                        City = dr["City"].ToString(),
                        State = dr["State"].ToString(),
                        PinCode = dr["PinCode"].ToString(),
                        Country = dr["Country"].ToString()
                    });
                }
            }
            return list;
        }

    }
}
