namespace NIAUNIVERSITYPANELAPI.Models
{
    public class LoginRequest
    {
        public string MobileNumber { get; set; }
    }

    public class VerifyOtpRequest
    {
        public string MobileNumber { get; set; }
        public string OTP { get; set; }
    }
}
