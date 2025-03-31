using DentRec.Application.DTOs.Procedure;
using Gridify;

namespace DentRec.Application.Interfaces
{
    public interface IProcedureService
    {
        Task<int> CreateProcedure(CreateProcedureDto dto);
        Task<int> UpdateProcedure(UpdateProcedureDto dto);
        Task<GetProcedureDto> GetProcedureById(int id);
        Task<bool> DeleteProcedure(int id);
        Task<Paging<GetProcedureDto>> GetProcedures(GridifyQuery gridifyQuery);

    }
}
