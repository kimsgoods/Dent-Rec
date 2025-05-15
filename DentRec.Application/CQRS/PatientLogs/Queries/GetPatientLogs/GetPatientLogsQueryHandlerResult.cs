namespace DentRec.Application.CQRS.PatientLogs.Queries.GetPatientLogs
{
    public class GetPatientLogsQueryHandlerResult
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Procedures { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime? ProcedureDate { get; set; }
        public string? Notes { get; set; }
        public decimal? Fee { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
    }
}
