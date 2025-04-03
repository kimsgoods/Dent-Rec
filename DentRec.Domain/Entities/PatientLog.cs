namespace DentRec.Domain.Entities
{
    public class PatientLog : BaseEntity
    {
        public int PatientId { get; set; }
        public int DentistId { get; set; }
        public DateTime ProcedureDate { get; set; }
        public string? Notes { get; set; }
        public decimal Fee { get; set; }
        public string PaymentStatus { get; set; } = "Pending";
        public Patient? Patient { get; set; }
        public Dentist? Dentist { get; set; }

        // Many-to-Many Relationship with Procedure
        public ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();
        // One-to-Many Relationship with Payment
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
