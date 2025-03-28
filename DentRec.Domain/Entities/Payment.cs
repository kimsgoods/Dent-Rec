namespace DentRec.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int PatientId { get; set; }
        public int PatientProcedureId { get; set; }

        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } = "Pending"; //Pending, Paid
        public string PaymentMethod { get; set; } = "Cash"; //Cash, Gcash

        public Patient? Patient { get; set; }
        public PatientProcedure? PatientProcedure { get; set; }
    }
}
