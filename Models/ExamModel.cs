namespace NIAUNIVERSITYPANELAPI.Models
{
    public class ExamModel
    {
        public int ExamId { get; set; }
        public int ProgramId { get; set; }  
        public int CourseId { get; set; }
        public string ProgramName { get; set; } = "";
        public string CourseName { get; set; } = "";
        public string ExamName { get; set; } = "";
        public bool Status { get; set; }
    }
}
