namespace DentRec.Application.DTOs.Payments
{
    public class GetPaymentDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientLogId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
