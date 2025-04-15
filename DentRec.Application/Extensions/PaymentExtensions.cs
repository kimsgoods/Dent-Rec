using DentRec.Application.DTOs.Payments;
using DentRec.Domain.Entities;

namespace DentRec.Application.Extensions
{
    public static class PaymentExtensions
    {
        public static GetPaymentDto ToDto(this Payment payment)
        {
            return new GetPaymentDto
            {
                Id = payment.Id,
                PatientId = payment.PatientId,
                PatientLogId = payment.PatientLogId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaymentDate = payment.CreatedOn
            };
        }

        public static GetPaymentDetailsDto ToDetailsDto(this Payment payment)
        {
            return new GetPaymentDetailsDto
            {
                Id = payment.Id,
                PatientId = payment.PatientId,
                PatientName = $"{payment.Patient?.FirstName} {payment.Patient?.LastName}",
                PatientLogId = payment.PatientLogId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaymentDate = payment.CreatedOn
            };
        }

        public static Payment ToEntity(this CreatePaymentDto dto)
        {
            return new Payment
            {
                PatientId = dto.PatientId,
                PatientLogId = dto.PatientLogId,               
                Amount = dto.Amount,
                PaymentMethod = dto.PaymentMethod
            };
        }
    }
}
