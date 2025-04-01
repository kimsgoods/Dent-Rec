namespace DentRec.Application.DTOs.PatientProcedure
{
    public class UpdatePatientProcedureDto
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public int? DentistId { get; set; }
        public int? ProcedureId { get; set; }
        public DateTime? ProcedureDate { get; set; }
        public string? Notes { get; set; }
    }
}
