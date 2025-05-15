using DentRec.Application.CRUD.DTOs.Report;
using Gridify;

namespace DentRec.Application.CRUD.Interfaces
{
    public interface IReportService
    {
        Task<Paging<GetDailyReportDto>> GetDailyReportAsync(GridifyQuery gridifyQuery);
    }
}
