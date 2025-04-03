using DentRec.Application.DTOs.PatientLog;
using Gridify;

namespace DentRec.Application.Interfaces
{
    public interface IPatientLogService
    {
        Task<int> CreatePatientLogAsync(CreatePatientLogDto dto);
        Task<int> UpdatePatientLogAsync(UpdatePatientLogDto dto);
        Task<GetPatientLogDetailsDto> GetPatientLogByIdAsync(int id);
        Task<bool> DeletePatientLogAsync(int id);
        Task<Paging<GetPatientLogDto>> GetPatientLogsAsync(GridifyQuery gridifyQuery);
    }
}
