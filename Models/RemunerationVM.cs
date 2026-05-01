namespace NIAUNIVERSITYPANELAPI.Models
{
    public class RemunerationVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string PanNo { get; set; }

        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string IFSCCode { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public int Count { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
