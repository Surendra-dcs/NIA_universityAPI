namespace NIAUNIVERSITYPANELAPI.Models
{
    public class StudentAdmissionModel
    {
        public int Id { get; set; }
        public string CollegeName { get; set; } = String.Empty;
        public string ProgramName { get; set; } = String.Empty;
        public string CourseName { get; set; } = String.Empty;
        public string StudentName { get; set; } = String.Empty;
        public string FatherName { get; set; } = String.Empty;
        public string MotherName { get; set; } = string.Empty;
        public string Mobile { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Gender { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string Category { get; set; } = String.Empty;
        public string Religion { get; set; } = String.Empty;
        public string Nationality { get; set; } = String.Empty;
        public string AadhaarId { get; set; } = String.Empty;
        public string AbcId { get; set; } = String.Empty;
        public string AddressLine1 { get; set; } = String.Empty;
        public string District { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string State { get; set; } = String.Empty;
        public string PinCode { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
    }
}
