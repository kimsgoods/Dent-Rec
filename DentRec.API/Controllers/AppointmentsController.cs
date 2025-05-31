using DentRec.Application.CQRS.Appointments.Commands.CreateAppointment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentRec.API.Controllers
{
    public class AppointmentsController(ISender sender) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> CreatePatientLog(CreateAppointmentCommand command)
        {
            var result = await sender.Send(command);
            return result;
        }
    }
}
