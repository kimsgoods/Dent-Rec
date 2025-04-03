using DentRec.Application.DTOs.PatientLog;
using DentRec.Domain.Entities;

namespace DentRec.Application.Extensions
{
    public static class PatientLogExtensions
    {
        public static GetPatientLogDto ToDto(this PatientLog patientLog)
        {
            return new GetPatientLogDto
            {
                Id = patientLog.Id,
                DentistId = patientLog.DentistId,
                DentistName = $"{patientLog.Dentist?.FirstName} {patientLog.Dentist?.LastName}".Trim(),
                PatientId = patientLog.PatientId,
                PatientName = $"{patientLog.Patient?.FirstName} {patientLog.Patient?.LastName}".Trim(),
                ProcedureId = patientLog.ProcedureId,
                ProcedureName = patientLog.Procedure?.Name ?? string.Empty,
                Gender = patientLog.Patient?.Gender ?? string.Empty,
                Age = patientLog.Patient?.DateOfBirth.HasValue == true
                    ? GetAgeDuringProcedure(patientLog.Patient.DateOfBirth.Value, patientLog.ProcedureDate)
                    : 0,
                Address = patientLog.Patient?.Address ?? string.Empty,
                ProcedureDate = patientLog.ProcedureDate,
                Fee = patientLog.Fee,
                Notes = patientLog.Notes,
                CreatedBy = patientLog.CreatedBy,
                ModifiedOn = patientLog.ModifiedOn,
                CreatedOn = patientLog.CreatedOn,
                ModifiedBy = patientLog.ModifiedBy
            };
        }


        public static PatientLog ToEntity(this CreatePatientLogDto patientDto)
        {
            return new PatientLog
            {
                PatientId = patientDto.PatientId,
                DentistId = patientDto.DentistId,
                ProcedureId = patientDto.ProcedureId,
                ProcedureDate = patientDto.ProcedureDate,
                Notes = patientDto.Notes
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
