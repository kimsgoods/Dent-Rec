using DentRec.Application.CRUD.DTOs.Dentist;
using DentRec.Application.CRUD.Extensions;
using DentRec.Application.CRUD.Interfaces;
using DentRec.Domain.Entities;
using Gridify;

namespace DentRec.Application.CRUD.Services
{
    public class DentistService(IExtendedRepository<Dentist> repository) : IDentistService
    {
        public async Task<int> CreateDentist(CreateDentistDto dto)
        {
            if (string.IsNullOrEmpty(dto.FirstName) || string.IsNullOrEmpty(dto.LastName))
            {
                throw new ArgumentException("First Name and Last Name are required.");
            }

            try
            {
                var newDentist = dto.ToEntity();
                repository.Add(newDentist);
                var result = await repository.SaveAsync(newDentist);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while saving the Dentist.", ex);
            }
        }

        public async Task<bool> DeleteDentist(int id)
        {
            var dentist = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Could not find Dentist with Id: {id}");

            repository.Remove(dentist);

            return await repository.SaveAsync(dentist) > 0;
        }

        public async Task<GetDentistDto> GetDentistById(int id)
        {
            var dentist = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Could not find Dentist with Id: {id}");
            return dentist.ToDto();
        }

        public async Task<Paging<GetDentistDto>> GetDentists(GridifyQuery gridifyQuery)
        {
            var dentists = await repository.GetPaginatedRecordsAsync(gridifyQuery);
            var result = new Paging<GetDentistDto>
            {
                Count = dentists.Count,
                Data = dentists.Data.Select(x => x.ToDto())
            };
            return result;
        }

        public async Task<int> UpdateDentist(UpdateDentistDto dto)
        {
            var dentist = await repository.GetByIdAsync(dto.Id)
                    ?? throw new KeyNotFoundException($"Could not find Dentist with Id: {dto.Id}");

            if (!string.IsNullOrEmpty(dto.FirstName)) dentist.FirstName = dto.FirstName;
            if (!string.IsNullOrEmpty(dto.LastName)) dentist.LastName = dto.LastName;
            if (!string.IsNullOrEmpty(dto.Email)) dentist.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.Phone)) dentist.Phone = dto.Phone;

            try
            {
                repository.Update(dentist);
                var result = await repository.SaveAsync(dentist);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while saving the Dentist.", ex);
            }
        }
    }
}
