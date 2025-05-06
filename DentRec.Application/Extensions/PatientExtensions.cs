using DentRec.Application.DTOs.Patient;
using DentRec.Domain.Entities;

namespace DentRec.Application.Extensions
{
    public static class PatientExtensions
    {
        public static GetPatientDetailsDto ToDetailsDto(this Patient patient)
        {
            return new GetPatientDetailsDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Address = patient.Address,
                Email = patient.Email,
                Gender = patient.Gender,
                Age = patient.Age,
                Phone = patient.Phone,
                CreatedBy = patient.CreatedBy,
                ModifiedOn = patient.ModifiedOn,
                CreatedOn = patient.CreatedOn,
                ModifiedBy = patient.ModifiedBy,
                PatientLogs = patient.PatientLogs.OrderByDescending(x => x.ProcedureDate).Select(x => x.ToDto()).ToList(),
                Payments = patient.Payments.OrderByDescending(x => x.CreatedOn).Select(x => x.ToDto()).ToList()
            };
        }

        public static GetPatientDto ToDto(this Patient patient)
        {
            return new GetPatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Address = patient.Address,
                Age = patient.Age,
                Email = patient.Email,
                Gender = patient.Gender,
                Phone = patient.Phone
            };
        }

        public static Patient ToEntity(this CreatePatientDto patientDto)
        {
            return new Patient
            {
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                Address = patientDto.Address,
                Age = patientDto.Age,
                Email = patientDto.Email,
                Gender = patientDto.Gender,
                Phone = patientDto.Phone
            };
        }
    }
}
