using DentRec.Application.DTOs.Payments;
using DentRec.Application.Extensions;
using DentRec.Application.Interfaces;
using DentRec.Domain.Entities;
using Gridify;
using System.Linq.Expressions;

namespace DentRec.Application.Services
{
    public class PaymentService(
        IRepository<Payment> paymentRepository,
        IRepository<Patient> patientRepository,
        IRepository<PatientLog> logRepository
    ) : IPaymentService
    {
        public async Task<int> CreatePayment(CreatePaymentDto dto)
        {

            var patientLog = await logRepository.GetByIdAsync(dto.PatientLogId, x => x.Payments);
            if (patientLog == null || patientLog.PatientId != dto.PatientId)
                throw new ArgumentException($"Patient log {dto.PatientLogId} does not belong to the given patient {dto.PatientId}.");

            var patient = await patientRepository.GetByIdAsync(dto.PatientId, x => x.Payments) ?? throw new KeyNotFoundException("Patient not found.");
            if (!Enum.TryParse<PaymentMethod>(dto.PaymentMethod, true, out var paymentMethod))
            {
                throw new ArgumentException($"Invalid paymentMethod: {dto.PaymentMethod}");
            }
            var newPayment = new Payment
            {
                Amount = dto.Amount,
                PaymentMethod = paymentMethod,
                PatientLogId = dto.PatientLogId
            };

            // Add Payment to Patient (for tracking)
            patient.Payments ??= new List<Payment>();
            patient.Payments.Add(newPayment);

            // Add Payment to PatientLog
            patientLog.Payments ??= new List<Payment>();
            patientLog.Payments.Add(newPayment);

            // Check if all payments fulfill the total fee
            var totalPaid = patientLog.Payments.Sum(p => p.Amount);
            if (totalPaid >= patientLog.Fee)
            {
                patientLog.PaymentStatus = "Paid";
            }
            else if (totalPaid > 0)
            {
                patientLog.PaymentStatus = "Partial";
            }
            else
            {
                patientLog.PaymentStatus = "Pending";
            }

            try
            {
                var result = await patientRepository.SaveAsync(patient);

                return newPayment.Id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while saving the Payment.", ex);
            }
        }

        public async Task<bool> DeletePayment(int id)
        {
            var payment = await paymentRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Could not find Payment with Id: {id}");

            // Get the associated PatientLog with its Payments
            var patientLog = await logRepository.GetByIdAsync(payment.PatientLogId, x => x.Payments)
                ?? throw new KeyNotFoundException($"Associated PatientLog not found for Payment {id}");

            paymentRepository.Remove(payment);

            var totalPaid = patientLog.Payments.Where(x => !x.IsDeleted).Sum(p => p.Amount);
            if (totalPaid >= patientLog.Fee)
            {
                patientLog.PaymentStatus = "Paid";
            }
            else if (totalPaid > 0)
            {
                patientLog.PaymentStatus = "Partial";
            }
            else
            {
                patientLog.PaymentStatus = "Pending";
            }


            await logRepository.SaveAsync(patientLog);

            return await paymentRepository.SaveAsync(payment) > 0;
        }

        public async Task<GetPaymentDetailsDto> GetPaymentById(int id)
        {
            var payment = await paymentRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Could not find Payment with Id: {id}");

            return payment.ToDetailsDto();
        }

        public async Task<Paging<GetPaymentDto>> GetPayments(GridifyQuery gridifyQuery)
        {
#pragma warning disable CS8603 // Possible null reference return.
            var payments = await paymentRepository.GetPaginatedRecordsAsync(gridifyQuery, x => x.Patient);
#pragma warning restore CS8603 // Possible null reference return.
            return new Paging<GetPaymentDto>
            {
                Count = payments.Count,
                Data = payments.Data.Select(x => x.ToDto())
            };
        }

        public async Task<int> UpdatePayment(UpdatePaymentDto dto)
        {
            var payment = await paymentRepository.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Could not find Payment with Id: {dto.Id}");

            if (dto.Amount != null) payment.Amount = dto.Amount.Value;
            if (dto.PaymentMethod != null)
            {
                if (!Enum.TryParse<PaymentMethod>(dto.PaymentMethod, true, out var paymentMethod))
                {
                    throw new ArgumentException($"Invalid paymentMethod: {dto.PaymentMethod}");
                }
                payment.PaymentMethod = paymentMethod;
            }
            

            try
            {
                paymentRepository.Update(payment);
                return await paymentRepository.SaveAsync(payment);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the Payment.", ex);
            }
        }
    }
}
