using DentRec.Application.DTOs.Procedure;
using DentRec.Domain.Entities;

namespace DentRec.Application.Extensions
{
    public static class ProcedureExtensions
    {
        public static GetProcedureDto ToDto(this Procedure procedure)
        {
            return new GetProcedureDto
            {
                Id = procedure.Id,
                Name = procedure.Name,
                Description = procedure.Description,
                Cost = procedure.Cost,
                EstimatedDuration = procedure.EstimatedDuration,
                CreatedBy = procedure.CreatedBy,
                ModifiedOn = procedure.ModifiedOn,
                CreatedOn = procedure.CreatedOn,
                ModifiedBy = procedure.ModifiedBy
            };
        }

        public static Procedure ToEntity(this CreateProcedureDto dto)
        {
            TimeSpan? duration = null;
            if (!string.IsNullOrWhiteSpace(dto.EstimatedDuration))
            {
                if (!TimeSpan.TryParse(dto.EstimatedDuration, out var parsedDuration))
                {
                    throw new ArgumentException("Invalid TimeSpan format. Use hh:mm:ss.");
                }
                duration = parsedDuration;
            }

            return new Procedure
            {
                Name = dto.Name,
                Cost = dto.Cost,
                Description = dto.Description,
                EstimatedDuration = duration
            };
        }
    }


}
