namespace NIAUNIVERSITYPANELAPI.Models
{
    public class ReEvaluationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RollNumber { get; set; } = "";
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = "";
        public string Reason { get; set; } = "";
        public string Status { get; set; } = "";
        public string CreatedAt { get; set; } = "";
        public string UpdatedAt { get; set; } = "";

        // Joined fields for display
        public string StudentName { get; set; } = "";
        public string CourseName { get; set; } = "";
        public string SemesterName { get; set; } = "";
    }
}
