namespace NIAUNIVERSITYPANELAPI.Models
{
    public class Rolllist
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public string RollNumber { get; set; }
        public string SemesterName { get; set; } = "";
        public string CourseName { get; set; } = "";
        public string ExamName { get; set; } = "";
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CandidateImagePath { get; set; } = "";
        public string SignatureImagePath { get; set; } = "";
        public string Gender { get; set; } = "";
        public string EnrollmentNumber { get; set; } = "";


    }
}