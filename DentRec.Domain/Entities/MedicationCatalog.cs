namespace DentRec.Domain.Entities
{
    public class MedicationCatalog : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Dosage { get; set; } = string.Empty; // Example: "500mg", "2 tablets"
        public string Instructions { get; set; } = string.Empty; // Example: "Take twice a day"
    }
}
