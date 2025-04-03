using DentRec.Application.DTOs.Procedure;
using DentRec.Domain.Entities;

namespace DentRec.Application.DTOs.PatientLog
{
    public class GetPatientLogDetailsDto : AuditFields
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string DentistName { get; set; } = string.Empty;

        public IEnumerable<GetProcedureDto> Procedures { get; set; } = [];
        public IEnumerable<Payment> Payments { get; set; } = [];
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime? ProcedureDate { get; set; }
        public string? Notes { get; set; }
        public decimal? Fee { get; set; }

        public string PaymentStatus = string.Empty;

        public int PatientId { get; set; }
        public int? DentistId { get; set; }
    }
}
