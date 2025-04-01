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
                DentistName = patientProcedure.Dentist != null ?
                    $"{patientProcedure.Dentist?.FirstName} {patientProcedure.Dentist?.LastName}" : string.Empty,
                PatientId = patientProcedure.PatientId,
                PatientName = patientProcedure.Patient != null ?
                    $"{patientProcedure.Patient.FirstName} {patientProcedure.Patient.LastName}" : string.Empty,
                ProcedureId = patientProcedure.ProcedureId,
                ProcedureName = patientProcedure.Procedure != null ?
                    patientProcedure.Procedure.Name : string.Empty,
                ProcedureDate = patientProcedure.ProcedureDate,
                Cost = patientProcedure.Cost,
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
    }
}
