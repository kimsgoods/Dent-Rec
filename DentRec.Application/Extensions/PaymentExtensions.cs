using DentRec.Application.CRUD.DTOs.Payment;
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
                PatientName = $"{payment.Patient?.FirstName} {payment.Patient?.LastName}".Trim(),
                PatientLogId = payment.PatientLogId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod.ToString(),
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
                PaymentMethod = payment.PaymentMethod.ToString(),
                PaymentDate = payment.CreatedOn
            };
        }
    }
}
