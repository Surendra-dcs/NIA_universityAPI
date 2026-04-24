namespace NIAUNIVERSITYPANELAPI.Models
{
    public class SubjectModel
    {
        public int SubjectId { get; set; }
        public int ProgramId { get; set; }
        public int CourseId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string programName { get; set; } = string.Empty;
        public string courseName { get; set; } = string.Empty;
        public string SubjectCode { get; set; } = string.Empty;
        public string SemPart { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
