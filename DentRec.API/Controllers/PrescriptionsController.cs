using DentRec.Application.DTOs.Prescription;
using DentRec.Application.Interfaces;
using Gridify;
using Microsoft.AspNetCore.Mvc;

namespace DentRec.API.Controllers
{
    public class PrescriptionsController(IPrescriptionService service) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult> CreatePrescription(CreatePrescriptionDto dto)
        {
            var result = await service.CreatePrescription(dto);
            return CreatedAtAction(nameof(GetPrescriptionById), new { Id = result }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPrescriptionDto>> GetPrescriptionById(int id)
        {
            var result = await service.GetPrescriptionById(id);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePrescription(UpdatePrescriptionDto dto)
        {
            await service.UpdatePrescription(dto);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Paging<GetPrescriptionDto>>> GetPrescriptions([FromQuery] GridifyQuery gridifyQuery)
        {
            var result = await service.GetPrescriptions(gridifyQuery);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePrescription(int id)
        {
            await service.DeletePrescription(id);
            return NoContent();
        }

    }
}
