namespace NIAUNIVERSITYPANELAPI.Models
{
    public class ExaminerModel
    {
        public int ExaminerID { get; set; }
        public string ExaminerName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string PANNo { get; set; }

        public string Qualification { get; set; }
        public string Specialization { get; set; }
        public decimal? TeachingExpUG { get; set; }
        public decimal? TeachingExpPG { get; set; }

        public string CollegeName { get; set; }
        public string CollegeAddress { get; set; }
        public string CollegeCity { get; set; }
        public string CollegeDistrict { get; set; }
        public string CollegeState { get; set; }
        public string CollegePinCode { get; set; }

        public string ResidentialAddress { get; set; }
        public string ResidentialCity { get; set; }
        public string ResidentialDistrict { get; set; }
        public string ResidentialState { get; set; }
        public string ResidentialPinCode { get; set; }

        public string BankName { get; set; }
        public string BankAccountNo { get; set; }
        public string IFSCCode { get; set; }
    }

    public class AssignPaperModel
    {
        public int ExaminerId { get; set; }
        public int CollegeId { get; set; }
        public int CourseId { get; set; }
        public int ExamId { get; set; }
        public int SubjectId { get; set; }
        public string PaperType { get; set; } = string.Empty;
    }
}
