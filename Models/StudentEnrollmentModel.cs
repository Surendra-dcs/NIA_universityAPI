namespace NIAUNIVERSITYPANELAPI.Models
{
    public class StudentEnrollmentModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string EnrollmentNumber { get; set; } = String.Empty;
        public string CollegeName { get; set; } = String.Empty;
        public string ProgramName { get; set; } = String.Empty;
        public string CourseName { get; set; } = String.Empty;
        public string StudentName { get; set; } = String.Empty;
        public string FatherName { get; set; } = String.Empty;
        public string MotherName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;

        public bool IsVerify { get; set; }   // ✅ ADD THIS
    }
}
