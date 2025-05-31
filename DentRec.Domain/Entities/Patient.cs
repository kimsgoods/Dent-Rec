namespace DentRec.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; } // Use age because birthday is not required
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public ICollection<PatientLog> PatientLogs { get; set; } = new List<PatientLog>();
        public ICollection<PatientPrescription> PatientPrescriptions { get; set; } = new List<PatientPrescription>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
