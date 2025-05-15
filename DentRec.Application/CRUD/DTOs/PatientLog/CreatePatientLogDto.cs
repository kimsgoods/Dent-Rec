namespace DentRec.Application.CRUD.DTOs.PatientLog
{
    public class CreatePatientLogDto
    {
        public required int PatientId { get; set; }
        public required int DentistId { get; set; }
        public List<CreatePatientLogProcedureDto> Procedures { get; set; } = [];
        public string? Notes { get; set; }
    }

    public class CreatePatientLogProcedureDto
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public string? Notes { get; set; }

    }
}
