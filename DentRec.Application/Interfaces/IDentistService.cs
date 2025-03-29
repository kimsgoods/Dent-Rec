using DentRec.Application.DTOs.Dentist;
using Gridify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentRec.Application.Interfaces
{
    public interface IDentistService
    {
        Task<int> CreateDentist(CreateDentistDto dto);
        Task<int> UpdateDentist(UpdateDentistDto dto);
        Task<GetDentistDto> GetDentistById(int id);
        Task<bool> DeleteDentist(int id);
        Task<Paging<GetDentistDto>> GetDentists(GridifyQuery gridifyQuery);
    }
}
