using DentRec.Application.DTOs.Report;
using DentRec.Application.Interfaces;
using Gridify;
using Microsoft.AspNetCore.Mvc;

namespace DentRec.API.Controllers
{
    public class ReportsController(IReportService service) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<Paging<GetDailyReportDto>>> GetDailyReportAsync([FromQuery] GridifyQuery gridifyQuery)
        {
            var result = await service.GetDailyReportAsync(gridifyQuery);
            return result;
        }
    }
}
