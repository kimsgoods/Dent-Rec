using DentRec.Application.DTOs.Prescription;
using Gridify;

namespace DentRec.Application.Interfaces
{
    public interface IPrescriptionService
    {
        Task<int> CreatePrescription(CreatePrescriptionDto dto);
        Task<int> UpdatePrescription(UpdatePrescriptionDto dto);
        Task<GetPrescriptionDto> GetPrescriptionById(int id);
        Task<bool> DeletePrescription(int id);
        Task<Paging<GetPrescriptionDto>> GetPrescriptions(GridifyQuery gridifyQuery);
    }
}
