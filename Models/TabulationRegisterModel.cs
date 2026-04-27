namespace NIAUNIVERSITYPANELAPI.Models
{
    public class TabulationRegisterModel
    {
        public string exam_name { get; set; }
        public string RollNumber { get; set; }
        public string enrollmentNo { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string subject_name { get; set; }
        public string semyearcode { get; set; }
        public string scriptId { get; set; }
        public int maxValue { get; set; }
        public int minValue { get; set; }
        public int Theory { get; set; }
        public string Practical { get; set; }
        public string Grace { get; set; }
        public string Requiredtoappear { get; set; }
        public string Distinction { get; set; }
        public int GrandTotal { get; set; }
        public string Result { get; set; }
    }
}
