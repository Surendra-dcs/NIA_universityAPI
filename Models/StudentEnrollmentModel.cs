namespace NIAUNIVERSITYPANELAPI.Models
{
    public class StudentEnrollmentModel
    {
        // ── Core enrollment fields (from existing sp_StudentEnrollmentList) ──
        public int Id { get; set; }
        public int UserId { get; set; }
        public string EnrollmentNumber { get; set; } = string.Empty;
        public string CollegeName { get; set; } = string.Empty;
        public string ProgramName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public bool IsVerify { get; set; }

        // ── Rich fields joined from tbl_StudentExamInfoMaster ───────────────
        public string FormNumber { get; set; } = string.Empty;
        public string RollNumber { get; set; } = string.Empty;
        public string AadhaarNumber { get; set; } = string.Empty;
        public string AbcId { get; set; } = string.Empty;
        public string Attempt { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string StudentNameHindi { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string FatherNameHindi { get; set; } = string.Empty;
        public string MotherName { get; set; } = string.Empty;
        public string MotherNameHindi { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string PwdCategory { get; set; } = string.Empty;
        public string Religion { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string CandidateImagePath { get; set; } = string.Empty;
        public string SignatureImagePath { get; set; } = string.Empty;
    }
}