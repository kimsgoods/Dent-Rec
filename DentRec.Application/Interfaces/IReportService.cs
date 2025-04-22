using DentRec.Application.DTOs.Report;
using Gridify;

namespace DentRec.Application.Interfaces
{
    public interface IReportService
    {
        Task<Paging<GetDailyReportDto>> GetDailyReportAsync(GridifyQuery gridifyQuery);
    }
}
