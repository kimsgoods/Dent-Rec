using DentRec.Application.DTOs.Patient;
using DentRec.Application.Extensions;
using DentRec.Application.Interfaces;
using DentRec.Domain.Entities;
using Gridify;
using Microsoft.EntityFrameworkCore;

namespace DentRec.Application.Services
{
    public class PatientService(IRepository<Patient> repository) : IPatientService
    {
        
        Func<IQueryable<Patient>, IQueryable<Patient>> includes = q => q.Include(p => p.PatientLogs).ThenInclude(pl=>pl.Procedures).Include(p=>p.Payments);
        public async Task<int> CreatePatient(CreatePatientDto dto)
        {
            if (String.IsNullOrEmpty(dto.FirstName) || String.IsNullOrEmpty(dto.LastName))
            {
                throw new ArgumentException("First Name and Last Name are required.");
            }

            try
            {
                var newPatient = dto.ToEntity();
                repository.Add(newPatient);
                var result = await repository.SaveAsync(newPatient);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while saving the patient.", ex);
            }
        }

        public async Task<bool> DeletePatient(int id)
        {
            var patient = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Could not find Patient with Id: {id}");

            repository.Remove(patient);

            return await repository.SaveAsync(patient) > 0;
        }

        public async Task<GetPatientDetailsDto> GetPatientById(int id)
        {
            var patient = await repository.GetByIdAsync(id, includes)
                ?? throw new KeyNotFoundException($"Could not find patient with Id: {id}");
            return patient.ToDetailsDto();
        }

        public async Task<Paging<GetPatientDto>> GetPatients(GridifyQuery gridifyQuery)
        {
            var patients = await repository.GetPaginatedRecordsAsync(gridifyQuery);
            var result = new Paging<GetPatientDto>
            {
                Count = patients.Count,
                Data = patients.Data.Select(x => x.ToDto())
            };
            return result;
        }

        public async Task<int> UpdatePatient(UpdatePatientDto dto)
        {
            var patient = await repository.GetByIdAsync(dto.Id)
                    ?? throw new KeyNotFoundException($"Could not find patient with Id: {dto.Id}");

            if (!String.IsNullOrEmpty(dto.FirstName)) patient.FirstName = dto.FirstName;
            if (!String.IsNullOrEmpty(dto.LastName)) patient.LastName = dto.LastName;
            if (!String.IsNullOrEmpty(dto.Gender)) patient.Gender = dto.Gender;
            if (!String.IsNullOrEmpty(dto.Email)) patient.Email = dto.Email;
            if (!String.IsNullOrEmpty(dto.Phone)) patient.Phone = dto.Phone;
            if (!String.IsNullOrEmpty(dto.Address)) patient.Address = dto.Address;
            if (dto.Age != null) patient.Age = dto.Age.Value;

            try
            {
                repository.Update(patient);
                var result = await repository.SaveAsync(patient);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while saving the patient.", ex);
            }
        }
    }
}
