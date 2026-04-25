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


    }
}
