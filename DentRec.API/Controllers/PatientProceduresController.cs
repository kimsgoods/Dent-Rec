using DentRec.Application.DTOs.PatientProcedure;
using DentRec.Application.Interfaces;
using Gridify;
using Microsoft.AspNetCore.Mvc;

namespace DentRec.API.Controllers
{
    public class PatientProceduresController(IPatientProcedureService service) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult> CreatePatientProcedure(CreatePatientProcedureDto dto)
        {
            var result = await service.CreatePatientProcedureAsync(dto);
            return CreatedAtAction(nameof(GetPatientProcedureById), new { Id = result }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPatientProcedureDto>> GetPatientProcedureById(int id)
        {
            var result = await service.GetPatientProcedureByIdAsync(id);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePatientProcedure(UpdatePatientProcedureDto dto)
        {
            await service.UpdatePatientProcedureAsync(dto);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Paging<GetPatientProcedureDto>>> GetPatientProcedures([FromQuery] GridifyQuery gridifyQuery)
        {
            var result = await service.GetPatientProceduresAsync(gridifyQuery);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatientProcedure(int id)
        {
            await service.DeletePatientProcedureAsync(id);
            return NoContent();
        }

    }
}
