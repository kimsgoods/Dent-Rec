namespace DentRec.Application.DTOs.PatientLog
{
    public class CreatePatientLogDto
    {
        public required int PatientId { get; set; }
        public required int DentistId { get; set; }
        public required int ProcedureId { get; set; }
        public required DateTime ProcedureDate { get; set; }
        public string? Notes { get; set; }
    }
}
