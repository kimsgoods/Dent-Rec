namespace DentRec.Application.CRUD.DTOs.Payment
{
    public class UpdatePaymentDto
    {
        public required int Id { get; set; }
        public decimal? Amount { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
