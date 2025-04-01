using DentRec.Application.DTOs.Dentist;
using DentRec.Application.Extensions;
using DentRec.Application.Interfaces;
using DentRec.Domain.Entities;
using Gridify;

namespace DentRec.Application.Services
{
    public class DentistService(IRepository<Dentist> repository) : IDentistService
    {
        public async Task<int> CreateDentist(CreateDentistDto dto)
        {
            if (String.IsNullOrEmpty(dto.FirstName) || String.IsNullOrEmpty(dto.LastName))
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

            if (!String.IsNullOrEmpty(dto.FirstName)) dentist.FirstName = dto.FirstName;
            if (!String.IsNullOrEmpty(dto.LastName)) dentist.LastName = dto.LastName;
            if (!String.IsNullOrEmpty(dto.Email)) dentist.Email = dto.Email;
            if (!String.IsNullOrEmpty(dto.Phone)) dentist.Phone = dto.Phone;

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
