namespace NIAUNIVERSITYPANELAPI.Models
{
    public class Course
    {
        public int Course_Id { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; } = string.Empty;
        public string Course_Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
