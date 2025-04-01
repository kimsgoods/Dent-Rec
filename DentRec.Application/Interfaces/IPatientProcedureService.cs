using DentRec.Application.DTOs.PatientProcedure;
using Gridify;

namespace DentRec.Application.Interfaces
{
    public interface IPatientProcedureService
    {
        Task<int> CreatePatientProcedureAsync(CreatePatientProcedureDto dto);
        Task<int> UpdatePatientProcedureAsync(UpdatePatientProcedureDto dto);
        Task<GetPatientProcedureDto> GetPatientProcedureByIdAsync(int id);
        Task<bool> DeletePatientProcedureAsync(int id);
        Task<Paging<GetPatientProcedureDto>> GetPatientProceduresAsync(GridifyQuery gridifyQuery);
    }
}
