namespace DentRec.Application.CRUD.DTOs.Payment
{
    public class GetPaymentDetailsDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientLogId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
