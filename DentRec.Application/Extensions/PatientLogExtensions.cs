using DentRec.Application.DTOs.PatientLog;
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
                Age = patientLog.Patient?.DateOfBirth.HasValue == true
                    ? GetAgeDuringProcedure(patientLog.Patient.DateOfBirth.Value, patientLog.ProcedureDate)
                    : 0,
                Address = patientLog.Patient?.Address ?? string.Empty,
                ProcedureDate = patientLog.ProcedureDate,
                Fee = patientLog.Fee,
                PaymentStatus = patientLog.PaymentStatus,
                Notes = patientLog.Notes,
                Procedures = patientLog.Procedures.Select(x=>x.ToDto()),
                CreatedBy = patientLog.CreatedBy,
                ModifiedOn = patientLog.ModifiedOn,
                CreatedOn = patientLog.CreatedOn,
                ModifiedBy = patientLog.ModifiedBy
            };
        }

        public static GetPatientLogDto ToDto(this PatientLog patientLog)
        {
            return new GetPatientLogDto
            {
                Id = patientLog.Id,               
                PatientName = $"{patientLog.Patient?.FirstName} {patientLog.Patient?.LastName}".Trim(),          
                Gender = patientLog.Patient?.Gender ?? string.Empty,                
                Age = patientLog.Patient?.DateOfBirth.HasValue == true
                    ? GetAgeDuringProcedure(patientLog.Patient.DateOfBirth.Value, patientLog.ProcedureDate)
                    : 0,
                Address = patientLog.Patient?.Address ?? string.Empty,
                ProcedureDate = patientLog.ProcedureDate,
                Fee = patientLog.Fee,
                Notes = patientLog.Notes,
                Procedures = string.Join(", ", patientLog.Procedures.Select(x => x.Name)),
                PaymentStatus = patientLog.PaymentStatus
            };
        }


        public static PatientLog ToEntity(this CreatePatientLogDto patientDto)
        {
            return new PatientLog
            {
                PatientId = patientDto.PatientId,
                DentistId = patientDto.DentistId,
                ProcedureDate = patientDto.ProcedureDate,
                Notes = patientDto.Notes,
                PaymentStatus = "Pending"
            };
        }

        private static int GetAgeDuringProcedure(DateTime dateOfBirth, DateTime dateOfProcedure)
        {            
            var age = dateOfProcedure.Year - dateOfBirth.Year;

            // Adjust if the birthday hasn't occurred yet this year
            if (dateOfBirth.Date > dateOfProcedure.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
