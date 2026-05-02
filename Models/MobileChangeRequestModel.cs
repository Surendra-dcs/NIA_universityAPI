namespace NIAUNIVERSITYPANELAPI.Models
{
    public class MobileChangeRequestModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string RollNumber { get; set; } = string.Empty;   // from tbl_StudentExamRollMaster
        public string OldMobile { get; set; } = string.Empty;
        public string NewMobile { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;       // Pending | Approved | Rejected
        public string CreatedAt { get; set; } = string.Empty;
    }
}
