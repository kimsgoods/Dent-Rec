using DentRec.Application.DTOs.PatientLog;
using DentRec.Application.Interfaces;
using Gridify;
using Microsoft.AspNetCore.Mvc;

namespace DentRec.API.Controllers
{
    public class PatientLogsController(IPatientLogService service) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult> CreatePatientLog(CreatePatientLogDto dto)
        {
            var result = await service.CreatePatientLogAsync(dto);
            return CreatedAtAction(nameof(GetPatientLogById), new { Id = result }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPatientLogDetailsDto>> GetPatientLogById(int id)
        {
            var result = await service.GetPatientLogByIdAsync(id);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePatientLog(UpdatePatientLogDto dto)
        {
            await service.UpdatePatientLogAsync(dto);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Paging<GetPatientLogDto>>> GetPatientLogs([FromQuery] GridifyQuery gridifyQuery)
        {
            var result = await service.GetPatientLogsAsync(gridifyQuery);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatientLog(int id)
        {
            await service.DeletePatientLogAsync(id);
            return NoContent();
        }

    }
}
