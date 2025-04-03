namespace DentRec.Domain.Entities
{
    public class PatientProcedure : BaseEntity
    {
        public int PatientId { get; set; }
        public int DentistId { get; set; }
        public int ProcedureId { get; set; }
        public DateTime ProcedureDate { get; set; }
        public string? Notes { get; set; }
        public decimal Fee { get; set; }

        public Patient? Patient { get; set; }
        public Dentist? Dentist { get; set; }
        public Procedure? Procedure { get; set; }
    }
}
