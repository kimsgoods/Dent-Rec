using DentRec.Application.CRUD.DTOs.Prescription;
using DentRec.Application.CRUD.Extensions;
using DentRec.Application.CRUD.Interfaces;
using DentRec.Domain.Entities;
using Gridify;

namespace DentRec.Application.CRUD.Services
{
    public class PrescriptionService(IExtendedRepository<Prescription> repository) : IPrescriptionService
    {
        public async Task<int> CreatePrescription(CreatePrescriptionDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
            {
                throw new ArgumentException("Name is required.");
            }
            if (string.IsNullOrEmpty(dto.Dosage))
            {
                throw new ArgumentException("Dosage is required.");
            }
            if (string.IsNullOrEmpty(dto.Instructions))
            {
                throw new ArgumentException("Instructions is required.");
            }
            try
            {
                var newPrescription = dto.ToEntity();
                repository.Add(newPrescription);
                var result = await repository.SaveAsync(newPrescription);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while saving the Prescription.", ex);
            }
        }

        public async Task<bool> DeletePrescription(int id)
        {
            var prescription = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Could not find Prescription with Id: {id}");

            repository.Remove(prescription);

            return await repository.SaveAsync(prescription) > 0;
        }

        public async Task<GetPrescriptionDto> GetPrescriptionById(int id)
        {
            var prescription = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Could not find Prescription with Id: {id}");
            return prescription.ToDto();
        }

        public async Task<Paging<GetPrescriptionDto>> GetPrescriptions(GridifyQuery gridifyQuery)
        {
            var prescriptions = await repository.GetPaginatedRecordsAsync(gridifyQuery);
            var result = new Paging<GetPrescriptionDto>
            {
                Count = prescriptions.Count,
                Data = prescriptions.Data.Select(x => x.ToDto())
            };
            return result;
        }

        public async Task<int> UpdatePrescription(UpdatePrescriptionDto dto)
        {
            var prescription = await repository.GetByIdAsync(dto.Id)
                    ?? throw new KeyNotFoundException($"Could not find Prescription with Id: {dto.Id}");


            if (!string.IsNullOrEmpty(dto.Name)) prescription.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.Description)) prescription.Description = dto.Description;
            if (!string.IsNullOrEmpty(dto.Dosage)) prescription.Dosage = dto.Dosage;
            if (!string.IsNullOrEmpty(dto.Instructions)) prescription.Instructions = dto.Instructions;

            try
            {
                repository.Update(prescription);
                var result = await repository.SaveAsync(prescription);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while saving the Prescription.", ex);
            }
        }
    }
}
