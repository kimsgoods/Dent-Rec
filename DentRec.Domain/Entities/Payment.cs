namespace DentRec.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int PatientId { get; set; }
        public int PatientLogId { get; set; }

        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = "Cash"; //Cash, Gcash

        public Patient? Patient { get; set; }
        public PatientLog? PatientLog { get; set; }
    }
}
