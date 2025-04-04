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
                CreatedBy = procedure.CreatedBy,
                ModifiedOn = procedure.ModifiedOn,
                CreatedOn = procedure.CreatedOn,
                ModifiedBy = procedure.ModifiedBy
            };
        }

        public static Procedure ToEntity(this CreateProcedureDto dto)
        {          
            return new Procedure
            {
                Name = dto.Name,
                Fee = dto.Fee,
                Description = dto.Description
            };
        }
    }


}
