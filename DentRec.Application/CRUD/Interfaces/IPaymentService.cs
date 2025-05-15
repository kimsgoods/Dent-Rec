using DentRec.Application.CRUD.DTOs.Payment;
using Gridify;

namespace DentRec.Application.CRUD.Interfaces
{
    public interface IPaymentService
    {
        Task<int> CreatePayment(CreatePaymentDto dto);
        Task<int> UpdatePayment(UpdatePaymentDto dto);
        Task<GetPaymentDetailsDto> GetPaymentById(int id);
        Task<bool> DeletePayment(int id);
        Task<Paging<GetPaymentDto>> GetPayments(GridifyQuery gridifyQuery);
    }
}
