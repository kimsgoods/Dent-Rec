using DentRec.Application.CRUD.DTOs;

namespace DentRec.Application.CRUD.DTOs.Prescription
{
    public class GetPrescriptionDto : AuditFields
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Dosage { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
    }
}
