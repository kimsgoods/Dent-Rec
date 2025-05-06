namespace DentRec.Application.DTOs.Payments
{
    public class CreatePaymentDto
    {
        public required int PatientId { get; set; }
        public required int PatientLogId { get; set; }
        public required decimal Amount { get; set; }
        public required string PaymentMethod { get; set; }
    }
}
