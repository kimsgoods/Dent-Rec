using DentRec.Application.DTOs.Patient;

namespace DentRec.Application.Interfaces
{
    public interface IPatientService
    {
        Task<int> CreatePatient(CreatePatientDto dto);
        Task<int> UpdatePatient(UpdatePatientDto dto);
        Task<GetPatientDto> GetPatientById(int id);
    }
}
