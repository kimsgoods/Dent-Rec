using DentRec.Domain.Enums;

namespace DentRec.Domain.Entities
{
    public class Procedure : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Fee { get; set; }
        public PricingType PricingType { get; set; }

        // Many-to-Many Relationship with PatientLog
        public ICollection<PatientLogProcedure> PatientLogProcedures { get; set; } = new List<PatientLogProcedure>();
    }
}
