using DentRec.Application.DTOs.Payments;
using DentRec.Application.Extensions;
using DentRec.Application.Interfaces;
using DentRec.Domain.Entities;
using Gridify;
using System.Linq.Expressions;

namespace DentRec.Application.Services
{
    public class PaymentService(
        IRepository<Payment> repository,
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
            var newPayment = new Payment
            {
                Amount = dto.Amount,
                PaymentMethod = dto.PaymentMethod,
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
            var payment = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Could not find Payment with Id: {id}");

            repository.Remove(payment);
            return await repository.SaveAsync(payment) > 0;
        }

        public async Task<GetPaymentDetailsDto> GetPaymentById(int id)
        {
            var payment = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Could not find Payment with Id: {id}");

            return payment.ToDetailsDto();
        }

        public async Task<Paging<GetPaymentDto>> GetPayments(GridifyQuery gridifyQuery)
        {
            var payments = await repository.GetPaginatedRecordsAsync(gridifyQuery);
            return new Paging<GetPaymentDto>
            {
                Count = payments.Count,
                Data = payments.Data.Select(x => x.ToDto())
            };
        }

        public async Task<int> UpdatePayment(UpdatePaymentDto dto)
        {
            var payment = await repository.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Could not find Payment with Id: {dto.Id}");

            if (dto.Amount != null) payment.Amount = dto.Amount.Value;
            if (dto.PaymentMethod != null) payment.PaymentMethod = dto.PaymentMethod;

            try
            {
                repository.Update(payment);
                return await repository.SaveAsync(payment);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the Payment.", ex);
            }
        }
    }
}
