using DentRec.Application.DTOs.Patient;
using DentRec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentRec.Application.Extensions
{
    public static class PatientExtensions
    {
        public static GetPatientDto ToDto(this Patient patient)
        {
            return new GetPatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Address = patient.Address,
                DateOfBirth = patient.DateOfBirth,
                Email = patient.Email,
                Gender = patient.Gender,
                Phone = patient.Phone,
                CreatedBy = patient.CreatedBy,
                ModifiedOn = patient.ModifiedOn,
                CreatedOn = patient.CreatedOn,
                ModifiedBy = patient.ModifiedBy             
            };
        }

        public static Patient ToEntity(this CreatePatientDto patientDto)
        {
            return new Patient
            {
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                Address = patientDto.Address,
                DateOfBirth = patientDto.DateOfBirth,
                Email = patientDto.Email,
                Gender = patientDto.Gender,
                Phone = patientDto.Phone
            };
        }
    }


}
