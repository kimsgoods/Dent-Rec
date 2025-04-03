namespace DentRec.Application.DTOs.PatientLog
{
    public class UpdatePatientLogDto
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public int? DentistId { get; set; }
        public DateTime? ProcedureDate { get; set; }
        public string? Notes { get; set; }
    }
}
