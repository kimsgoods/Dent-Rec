using DentRec.Application.DTOs.Patient;
using DentRec.Application.Interfaces;
using DentRec.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DentRec.API.Controllers
{
    public class PatientsController(IPatientService service) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> CreatePatient(CreatePatientDto dto)
        {
            var result = await service.CreatePatient(dto);
            return CreatedAtAction(nameof(GetPatientById), new { Id = result });
        }

        [HttpGet]
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
    }
}
