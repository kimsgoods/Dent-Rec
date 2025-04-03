using DentRec.Application.DTOs.PatientProcedure;
using DentRec.Domain.Entities;

namespace DentRec.Application.Extensions
{
    public static class PatientProcedureExtensions
    {
        public static GetPatientProcedureDto ToDto(this PatientProcedure patientProcedure)
        {
            return new GetPatientProcedureDto
            {
                Id = patientProcedure.Id,
                DentistId = patientProcedure.DentistId,
                DentistName = $"{patientProcedure.Dentist?.FirstName} {patientProcedure.Dentist?.LastName}".Trim(),
                PatientId = patientProcedure.PatientId,
                PatientName = $"{patientProcedure.Patient?.FirstName} {patientProcedure.Patient?.LastName}".Trim(),
                ProcedureId = patientProcedure.ProcedureId,
                ProcedureName = patientProcedure.Procedure?.Name ?? string.Empty,
                Gender = patientProcedure.Patient?.Gender ?? string.Empty,
                Age = patientProcedure.Patient?.DateOfBirth.HasValue == true
                    ? GetAgeDuringProcedure(patientProcedure.Patient.DateOfBirth.Value, patientProcedure.ProcedureDate)
                    : 0,
                Address = patientProcedure.Patient?.Address ?? string.Empty,
                ProcedureDate = patientProcedure.ProcedureDate,
                Fee = patientProcedure.Fee,
                Notes = patientProcedure.Notes,
                CreatedBy = patientProcedure.CreatedBy,
                ModifiedOn = patientProcedure.ModifiedOn,
                CreatedOn = patientProcedure.CreatedOn,
                ModifiedBy = patientProcedure.ModifiedBy
            };
        }


        public static PatientProcedure ToEntity(this CreatePatientProcedureDto patientDto)
        {
            return new PatientProcedure
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
