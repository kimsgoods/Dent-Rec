namespace DentRec.Application.DTOs.Payments
{
    public class UpdatePaymentDto
    {
        public required int Id { get; set; }
        public decimal? Amount { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
