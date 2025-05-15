using DentRec.Application.CQRS.PatientLogs.Commands.CreatePatientLog;
using DentRec.Application.CQRS.PatientLogs.Commands.DeletePatientLog;
using DentRec.Application.CQRS.PatientLogs.Commands.UpdatePatientLog;
using DentRec.Application.CQRS.PatientLogs.Queries.GetPatientLogDetailsById;
using DentRec.Application.CQRS.PatientLogs.Queries.GetPatientLogs;
using Gridify;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentRec.API.Controllers
{
    public class PatientLogsController(ISender sender) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult> CreatePatientLog(CreatePatientLogCommand command)
        {
            var result = await sender.Send(command);
            return CreatedAtAction(nameof(GetPatientLogById), new { Id = result }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPatientLogDetailsByIdQueryResult>> GetPatientLogById(int id)
        {
            var query = new GetPatientLogDetailsByIdQuery(id);
            var result = await sender.Send(query);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePatientLog(UpdatePatientLogCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Paging<GetPatientLogsQueryHandlerResult>>> GetPatientLogs([FromQuery] GridifyQuery gridifyQuery)
        {
            var query = new GetPatientLogsQuery(gridifyQuery);
            var result = await sender.Send(query);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatientLog(int id)
        {
            var command = new DeletePatientLogCommand(id);
            await sender.Send(command);
            return NoContent();
        }

    }
}
