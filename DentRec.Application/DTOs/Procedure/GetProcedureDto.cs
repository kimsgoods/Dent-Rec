namespace DentRec.Application.DTOs.Procedure
{
    public class GetProcedureDto : AuditFields
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Fee { get; set; }
        public string PricingType { get; set; } = string.Empty;
    }
}
