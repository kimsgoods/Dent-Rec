namespace DentRec.Application.DTOs.PatientLog
{
    public class GetPatientLogDto : AuditFields
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string DentistName { get; set; } = string.Empty;
        public string ProcedureName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime? ProcedureDate { get; set; }
        public string? Notes { get; set; }
        public decimal? Fee { get; set; }
        public string PaymentStatus = string.Empty;

        public int PatientId { get; set; }
        public int? DentistId { get; set; }
        public int? ProcedureId { get; set; }
    }
}
