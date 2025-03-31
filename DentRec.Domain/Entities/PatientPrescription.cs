namespace DentRec.Domain.Entities
{
    public class PatientPrescription : BaseEntity
    {
        public int PatientId { get; set; }
        public int DentistId { get; set; }
        public int MedicationId { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string? Notes { get; set; }

        public Patient? Patient { get; set; }
        public Dentist? Dentist { get; set; }
        public Prescription? Medication { get; set; }
    }

}
