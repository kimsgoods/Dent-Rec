using DentRec.Application.CRUD.DTOs;
using DentRec.Application.CRUD.DTOs.Payment;

namespace DentRec.Application.CRUD.DTOs.PatientLog
{
    public class GetPatientLogDetailsDto : AuditFields
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
        public int? DentistId { get; set; }
        public string DentistName { get; set; } = string.Empty;

        public DateTime? ProcedureDate { get; set; }
        public string? Notes { get; set; }
        public decimal? Fee { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;

        public IEnumerable<GetPatientLogProceduresDto> Procedures { get; set; } = [];
        public IEnumerable<GetPaymentDto> Payments { get; set; } = [];
    }
}
