namespace DentRec.Domain.Entities
{
    public class Procedure : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Fee { get; set; }
        public TimeSpan? EstimatedDuration { get; set; }

        // Many-to-Many Relationship with PatientLog
        public ICollection<PatientLog> PatientLogs { get; set; } = new List<PatientLog>();
    }
}
