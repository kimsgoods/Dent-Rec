using DentRec.Domain.Enums;

namespace DentRec.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int PatientId { get; set; }
        public int PatientLogId { get; set; }

        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public Patient? Patient { get; set; }
        public PatientLog? PatientLog { get; set; }
    }
}
