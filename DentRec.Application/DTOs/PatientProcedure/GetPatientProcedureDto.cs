namespace DentRec.Application.DTOs.PatientProcedure
{
    public class GetPatientProcedureDto : AuditFields
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string DentistName { get; set; } = string.Empty;
        public string ProcedureName { get; set; } = string.Empty;
        public DateTime? ProcedureDate { get; set; }
        public string? Notes { get; set; }
        public decimal? Cost { get; set; }

        public int PatientId { get; set; }
        public int? DentistId { get; set; }
        public int? ProcedureId { get; set; }
    }
}
