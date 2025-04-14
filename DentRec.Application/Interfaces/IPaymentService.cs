using DentRec.Application.DTOs.Payments;
using Gridify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentRec.Application.Interfaces
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
