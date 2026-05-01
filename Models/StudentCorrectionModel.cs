    namespace NIAUNIVERSITYPANELAPI.Models
{
    public class StudentCorrectionModel
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string StudentName { get; set; } = String.Empty;
        public string FatherName { get; set; } = String.Empty;
        public string MotherName { get; set; } = string.Empty;
        public string AlternateMobile { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string MainMobile { get; set; } = string.Empty;

        // ── Image paths from S3 (via UploadedDocuments join) ──────────
        public string CandidateImagePath { get; set; } = string.Empty;
        public string SignatureImagePath { get; set; } = string.Empty;
    }

    public class UpdateMobileModel
        {
            public int Id { get; set; }
            public int userId { get; set; }
            public string Mobile { get; set; } = string.Empty;
        }
}
