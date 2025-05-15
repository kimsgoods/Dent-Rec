using DentRec.Application.CRUD.DTOs.Patient;
using DentRec.Application.CRUD.Interfaces;
using Gridify;
using Microsoft.AspNetCore.Mvc;

namespace DentRec.API.Controllers
{
    public class PatientsController(IPatientService service) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult> CreatePatient(CreatePatientDto dto)
        {
            var result = await service.CreatePatient(dto);
            return CreatedAtAction(nameof(GetPatientById), new { Id = result }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPatientDetailsDto>> GetPatientById(int id)
        {
            var result = await service.GetPatientById(id);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePatient(UpdatePatientDto dto)
        {
            var result = await service.UpdatePatient(dto);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Paging<GetPatientDto>>> GetPatients([FromQuery] GridifyQuery gridifyQuery)
        {
            var result = await service.GetPatients(gridifyQuery);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var result = await service.DeletePatient(id);
            return NoContent();
        }
    }
}
