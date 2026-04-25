namespace NIAUNIVERSITYPANELAPI.Models
{
    public class StudentAdmissionModel
    {
        public int Id { get; set; }
        public string FormNumber { get; set; } = String.Empty;
        public string StudentName { get; set; } = String.Empty;
        public string FatherName { get; set; } = String.Empty;
        public string MotherName { get; set; } = string.Empty;
        public string Mobile { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Program { get; set; } = String.Empty;
        public string Course { get; set; } = String.Empty;
        public string Gender { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
    }
}
