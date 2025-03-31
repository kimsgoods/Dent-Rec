namespace DentRec.Application.DTOs.Prescription
{
    public class CreatePrescriptionDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Dosage { get; set; }
        public required string Instructions { get; set; }
    }
}
