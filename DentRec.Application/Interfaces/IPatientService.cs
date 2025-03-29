using DentRec.Application.DTOs.Patient;
using Gridify;

namespace DentRec.Application.Interfaces
{
    public interface IPatientService
    {
        Task<int> CreatePatient(CreatePatientDto dto);
        Task<int> UpdatePatient(UpdatePatientDto dto);
        Task<GetPatientDto> GetPatientById(int id);
        Task<Paging<GetPatientDto>> GetPatients(GridifyQuery gridifyQuery);
    }
}
