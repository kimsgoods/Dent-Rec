namespace DentRec.Application.DTOs.PatientLog
{
    public class CreatePatientLogDto
    {
        public required int PatientId { get; set; }
        public required int DentistId { get; set; }
        public List<int> ProcedureIds { get; set; } = new List<int>();
        public string? Notes { get; set; }
    }
}
