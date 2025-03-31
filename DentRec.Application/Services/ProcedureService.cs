using DentRec.Application.DTOs.Procedure;
using DentRec.Application.Extensions;
using DentRec.Application.Interfaces;
using DentRec.Domain.Entities;
using Gridify;

namespace DentRec.Application.Services
{
    public class ProcedureService(IRepository<Procedure> repository) : IProcedureService
    {
        public async Task<int> CreateProcedure(CreateProcedureDto dto)
        {
            if (String.IsNullOrEmpty(dto.Name))
            {
                throw new ArgumentException("Name is required.");
            }
            try
            {
                var newProcedure = dto.ToEntity();
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

            return await repository.SaveAsync(Procedure) > 1;
        }

        public async Task<GetProcedureDto> GetProcedureById(int id)
        {
            var Procedure = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Could not find Procedure with Id: {id}");
            return Procedure.ToDto();
        }

        public async Task<Paging<GetProcedureDto>> GetProcedures(GridifyQuery gridifyQuery)
        {
            var Procedures = await repository.GetPaginatedRecords(gridifyQuery);
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

            var duration = new TimeSpan();
            if (dto.EstimatedDuration != null && !TimeSpan.TryParse(dto.EstimatedDuration, out duration))
            {
                throw new ArgumentException("Invalid TimeSpan format. Use hh:mm:ss.");
            }
            if (!String.IsNullOrEmpty(dto.Name)) procedure.Name = dto.Name;
            if (!String.IsNullOrEmpty(dto.Description)) procedure.Description = dto.Description;
            if (dto.Cost.HasValue) procedure.Cost = (decimal)dto.Cost;
            if (!String.IsNullOrEmpty(dto.EstimatedDuration)) procedure.EstimatedDuration = duration;

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
