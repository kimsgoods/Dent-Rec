using DentRec.Application.DTOs.Patient;
using DentRec.Application.Interfaces;
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
        public async Task<ActionResult<GetPatientDto>> GetPatientById(int id)
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
    }
}
