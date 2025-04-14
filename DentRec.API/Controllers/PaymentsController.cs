using DentRec.Application.DTOs.Payments;
using DentRec.Application.Interfaces;
using Gridify;
using Microsoft.AspNetCore.Mvc;

namespace DentRec.API.Controllers
{
    public class PaymentsController(IPaymentService service) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult> CreatePayment(CreatePaymentDto dto)
        {
            var result = await service.CreatePayment(dto);
            return CreatedAtAction(nameof(GetPaymentById), new { Id = result }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPaymentDetailsDto>> GetPaymentById(int id)
        {
            var result = await service.GetPaymentById(id);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayment(UpdatePaymentDto dto)
        {
            var result = await service.UpdatePayment(dto);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Paging<GetPaymentDto>>> GetPayments([FromQuery] GridifyQuery gridifyQuery)
        {
            var result = await service.GetPayments(gridifyQuery);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePayment(int id)
        {
            var result = await service.DeletePayment(id);
            return NoContent();
        }

    }
}
