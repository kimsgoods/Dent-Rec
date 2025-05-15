using DentRec.Application.CRUD.DTOs.PatientLog;
using DentRec.Domain.Entities;

namespace DentRec.Application.Extensions
{
    public static class PatientLogExtensions
    {
        public static GetPatientLogDetailsDto ToDetailsDto(this PatientLog patientLog)
        {
            return new GetPatientLogDetailsDto
            {
                Id = patientLog.Id,
                DentistId = patientLog.DentistId,
                DentistName = $"{patientLog.Dentist?.FirstName} {patientLog.Dentist?.LastName}".Trim(),
                PatientId = patientLog.PatientId,
                PatientName = $"{patientLog.Patient?.FirstName} {patientLog.Patient?.LastName}".Trim(),
                Gender = patientLog.Patient?.Gender ?? string.Empty,
                Age = patientLog.PatientAge,
                Address = patientLog.Patient?.Address ?? string.Empty,
                ProcedureDate = patientLog.ProcedureDate,
                Fee = patientLog.Fee,
                PaymentStatus = patientLog.PaymentStatus,
                Notes = patientLog.Notes,
                Procedures = patientLog.PatientLogProcedures.Select(x => x.ToDto()),
                CreatedBy = patientLog.CreatedBy,
                ModifiedOn = patientLog.ModifiedOn,
                CreatedOn = patientLog.CreatedOn,
                ModifiedBy = patientLog.ModifiedBy,
                Payments = patientLog.Payments.Select(x => x.ToDto())
            };
        }
        public static GetPatientLogDto ToDto(this PatientLog patientLog)
        {
            return new GetPatientLogDto
            {
                Id = patientLog.Id,
                PatientId = patientLog.PatientId,
                PatientName = $"{patientLog.Patient?.FirstName} {patientLog.Patient?.LastName}".Trim(),
                Gender = patientLog.Patient?.Gender ?? string.Empty,
                Age = patientLog.PatientAge,
                Address = patientLog.Patient?.Address ?? string.Empty,
                ProcedureDate = patientLog.ProcedureDate,
                Fee = patientLog.Fee,
                Notes = patientLog.Notes,
                Procedures = string.Join(", ", patientLog.PatientLogProcedures.Select(x => x.Procedure!.Name)),
                PaymentStatus = patientLog.PaymentStatus
            };
        }

        public static GetPatientLogProceduresDto ToDto(this PatientLogProcedure patientLogProcedure)
        {
            return new GetPatientLogProceduresDto
            {
                Procedure = patientLogProcedure.Procedure.ToDto(),
                AdjustedFee = patientLogProcedure.AdjustedFee,
                Notes = patientLogProcedure.Notes,
                Quantity = patientLogProcedure.Quantity
            };
        }

        public static PatientLog ToEntity(this CreatePatientLogDto patientDto)
        {
            return new PatientLog
            {
                PatientId = patientDto.PatientId,
                DentistId = patientDto.DentistId,
                ProcedureDate = DateTime.Now,
                Notes = patientDto.Notes,
                PaymentStatus = "Pending"
            };
        }
    }
}
