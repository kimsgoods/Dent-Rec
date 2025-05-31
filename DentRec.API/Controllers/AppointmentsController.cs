using DentRec.Application.CQRS.Appointments.Commands.CancelAppointment;
using DentRec.Application.CQRS.Appointments.Commands.CompleteAppointment;
using DentRec.Application.CQRS.Appointments.Commands.CreateAppointment;
using DentRec.Application.CQRS.Appointments.Commands.RescheduleAppointment;
using DentRec.Application.CQRS.Appointments.Commands.UpdateAppointment;
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

        [HttpPut("{id}/reschedule")]
        public async Task<ActionResult<int>> RescheduleAppointment(int id, DateTime appointmentDateTime)
        {
            var result = await sender.Send(new RescheduleAppointmentCommand { Id = id, NewAppointmentDateTime = appointmentDateTime });
            return result;
        }

        [HttpPut("{id}/notes")]
        public async Task<ActionResult<int>> UpdateAppointmentNotes(int id, string notes)
        {
            var result = await sender.Send(new UpdateAppointmentNotesCommand { Id = id, Notes = notes });
            return result;
        }


    }
}
