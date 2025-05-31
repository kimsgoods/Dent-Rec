using DentRec.Application.CQRS.Appointments.Commands.CancelAppointment;
using DentRec.Application.CQRS.Appointments.Commands.CompleteAppointment;
using DentRec.Application.CQRS.Appointments.Commands.CreateAppointment;
using DentRec.Application.CQRS.Appointments.Commands.RescheduleAppointment;
using DentRec.Application.CQRS.Appointments.Commands.UpdateAppointment;
using DentRec.Application.CQRS.Appointments.Queries.GetAppointmentById;
using DentRec.Application.CQRS.Appointments.Queries.GetAppointments;
using Gridify;
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

        [HttpPut("{id}/cancel")]
        public async Task<ActionResult<int>> CancelAppointment(int id)
        {
            var result = await sender.Send(new CancelAppointmentCommand { Id = id });
            return result;
        }

        [HttpPut("{id}/complete")]
        public async Task<ActionResult<int>> CompleteAppointment(int id)
        {
            var result = await sender.Send(new CompleteAppointmentCommand { Id = id });
            return result;
        }

        [HttpPut("reschedule")]
        public async Task<ActionResult<int>> RescheduleAppointment(RescheduleAppointmentCommand command)
        {
            var result = await sender.Send(command);
            return result;
        }

        [HttpPut("notes")]
        public async Task<ActionResult<int>> UpdateAppointmentNotes(UpdateAppointmentNotesCommand command)
        {
            var result = await sender.Send(command);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetAppointmentByIdQueryResult>> GetAppointmentDetails(int id)
        {
            var result = await sender.Send(new GetAppointmentByIdQuery { Id = id});
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<Paging<GetAppointmentsQueryResult>>> GetAppointments([FromQuery] GridifyQuery gridifyQuery)
        {
            var result = await sender.Send(new GetAppointmentsQuery(gridifyQuery));
            return result;
        }


    }
}
