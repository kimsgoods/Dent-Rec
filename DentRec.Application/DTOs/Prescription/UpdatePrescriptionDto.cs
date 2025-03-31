namespace DentRec.Application.DTOs.Prescription
{
    public class UpdatePrescriptionDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Dosage { get; set; }
        public string? Instructions { get; set; }
    }
}
