using DentRec.Application.CRUD.DTOs.Procedure;
using DentRec.Application.CRUD.Interfaces;
using Gridify;
using Microsoft.AspNetCore.Mvc;

namespace DentRec.API.Controllers
{
    public class ProceduresController(IProcedureService service) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult> CreateProcedure(CreateProcedureDto dto)
        {
            var result = await service.CreateProcedure(dto);
            return CreatedAtAction(nameof(GetProcedureById), new { Id = result }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProcedureDto>> GetProcedureById(int id)
        {
            var result = await service.GetProcedureById(id);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProcedure(UpdateProcedureDto dto)
        {
            var result = await service.UpdateProcedure(dto);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Paging<GetProcedureDto>>> GetProcedures([FromQuery] GridifyQuery gridifyQuery)
        {
            var result = await service.GetProcedures(gridifyQuery);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProcedure(int id)
        {
            var result = await service.DeleteProcedure(id);
            return NoContent();
        }

    }
}
