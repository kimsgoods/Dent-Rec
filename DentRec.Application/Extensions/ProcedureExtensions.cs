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
                Fee = procedure.Fee,
                PricingType = procedure.PricingType.ToString(),
                CreatedBy = procedure.CreatedBy,
                ModifiedOn = procedure.ModifiedOn,
                CreatedOn = procedure.CreatedOn,
                ModifiedBy = procedure.ModifiedBy
            };
        }

        public static Procedure ToEntity(this CreateProcedureDto dto)
        {
            if (!Enum.TryParse<PricingType>(dto.PricingType, true, out var pricingType))
            {
                throw new ArgumentException($"Invalid pricingType: {dto.PricingType}");
            }
            return new Procedure
            {
                Name = dto.Name,
                Fee = dto.Fee,
                Description = dto.Description,
                PricingType = pricingType
            };
        }
    }


}
