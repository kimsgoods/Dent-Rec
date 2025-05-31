using DentRec.Application.CRUD.DTOs.Procedure;
using DentRec.Application.CRUD.Extensions;
using DentRec.Application.CRUD.Interfaces;
using DentRec.Domain.Entities;
using DentRec.Domain.Enums;
using Gridify;

namespace DentRec.Application.CRUD.Services
{
    public class ProcedureService(IExtendedRepository<Procedure> repository) : IProcedureService
    {
        public async Task<int> CreateProcedure(CreateProcedureDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
            {
                throw new ArgumentException("Name is required.");
            }
            if (string.IsNullOrEmpty(dto.PricingType))
            {
                throw new ArgumentException("PricingType is required.");
            }

            var newProcedure = dto.ToEntity();
            try
            {
                repository.Add(newProcedure);
                var result = await repository.SaveAsync(newProcedure);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while saving the Procedure.", ex);
            }
        }

        public async Task<bool> DeleteProcedure(int id)
        {
            var Procedure = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Could not find Procedure with Id: {id}");

            repository.Remove(Procedure);

            return await repository.SaveAsync(Procedure) > 0;
        }

        public async Task<GetProcedureDto> GetProcedureById(int id)
        {
            var Procedure = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Could not find Procedure with Id: {id}");
            return Procedure.ToDto();
        }

        public async Task<Paging<GetProcedureDto>> GetProcedures(GridifyQuery gridifyQuery)
        {
            var Procedures = await repository.GetPaginatedRecordsAsync(gridifyQuery);
            var result = new Paging<GetProcedureDto>
            {
                Count = Procedures.Count,
                Data = Procedures.Data.Select(x => x.ToDto())
            };
            return result;
        }

        public async Task<int> UpdateProcedure(UpdateProcedureDto dto)
        {
            var procedure = await repository.GetByIdAsync(dto.Id)
                    ?? throw new KeyNotFoundException($"Could not find Procedure with Id: {dto.Id}");

            if (!string.IsNullOrEmpty(dto.Name)) procedure.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.Description)) procedure.Description = dto.Description;
            if (!string.IsNullOrEmpty(dto.PricingType))
            {
                if (!Enum.TryParse<PricingType>(dto.PricingType, true, out var pricingType))
                {
                    throw new ArgumentException($"Invalid pricingType: {dto.PricingType}");
                }
                procedure.PricingType = pricingType;
            }
            if (dto.Fee.HasValue) procedure.Fee = (decimal)dto.Fee;

            try
            {
                repository.Update(procedure);
                var result = await repository.SaveAsync(procedure);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while saving the Procedure.", ex);
            }
        }
    }
}
