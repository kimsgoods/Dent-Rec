namespace DentRec.Application.DTOs.Procedure
{
    public class CreateProcedureDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Fee { get; set; }
        public required string PricingType { get; set; }
    }
}
