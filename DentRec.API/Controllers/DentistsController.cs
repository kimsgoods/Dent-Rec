using DentRec.Application.DTOs.Dentist;
using DentRec.Application.Interfaces;
using Gridify;
using Microsoft.AspNetCore.Mvc;

namespace DentRec.API.Controllers
{
    public class DentistsController(IDentistService service) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult> CreateDentist(CreateDentistDto dto)
        {
            var result = await service.CreateDentist(dto);
            return CreatedAtAction(nameof(GetDentistById), new { Id = result }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetDentistDto>> GetDentistById(int id)
        {
            var result = await service.GetDentistById(id);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDentist(UpdateDentistDto dto)
        {
            var result = await service.UpdateDentist(dto);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Paging<GetDentistDto>>> GetDentists([FromQuery] GridifyQuery gridifyQuery)
        {
            var result = await service.GetDentists(gridifyQuery);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDentist(int id)
        {
            var result = await service.DeleteDentist(id);
            return NoContent();
        }

    }
}
