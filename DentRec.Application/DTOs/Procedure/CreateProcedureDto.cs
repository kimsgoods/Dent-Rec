namespace DentRec.Application.DTOs.Procedure
{
    public class CreateProcedureDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Cost { get; set; }

        [TimeSpanFormat]
        public string? EstimatedDuration { get; set; }
    }
}
