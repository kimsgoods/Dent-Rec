namespace DentRec.Domain.Entities
{
    public class Dentist : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public ICollection<PatientProcedure>? PatientProcedures { get; set; }
        public ICollection<PatientPrescription>? PatientPrescriptions { get; set; }
    }
}
