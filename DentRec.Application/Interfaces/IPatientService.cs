using DentRec.Application.DTOs.Patient;
using Gridify;

namespace DentRec.Application.Interfaces
{
    public interface IPatientService
    {
        Task<int> CreatePatient(CreatePatientDto dto);
        Task<int> UpdatePatient(UpdatePatientDto dto);
        Task<GetPatientDetailsDto> GetPatientById(int id);
        Task<bool> DeletePatient(int id);
        Task<Paging<GetPatientDto>> GetPatients(GridifyQuery gridifyQuery);
    }
}
