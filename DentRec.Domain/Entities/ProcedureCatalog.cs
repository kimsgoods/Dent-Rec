namespace DentRec.Domain.Entities
{
    public class ProcedureCatalog : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Cost { get; set; }
        public TimeSpan? EstimatedDuration { get; set; }
    }
}
