using System.Data;
using Microsoft.Data.SqlClient;
namespace NIAUNIVERSITYPANELAPI.Service
{
 
    public interface IAuthService
    {
        void GenerateOtp(string mobile);
        bool VerifyOtp(string mobile, string otp);
    }

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public void GenerateOtp(string mobile)
        {
            var otp = new Random().Next(100000, 999999).ToString();

            using SqlConnection con = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            using SqlCommand cmd = new SqlCommand("sp_GenerateOTP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MobileNumber", mobile);
            cmd.Parameters.AddWithValue("@OTP", otp);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public bool VerifyOtp(string mobile, string otp)
        {
            using SqlConnection con = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            using SqlCommand cmd = new SqlCommand("sp_VerifyOTP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MobileNumber", mobile);
            cmd.Parameters.AddWithValue("@OTP", otp);
            con.Open();
            var reader = cmd.ExecuteReader();
            return reader.HasRows;
        }
    }
}
