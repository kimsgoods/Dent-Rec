using DentRec.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DentRec.Application.CQRS.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentNotesCommandHandler(IExtendedRepository<Appointment> appointmentRepository,
        ILogger<UpdateAppointmentNotesCommand> logger) : IRequestHandler<UpdateAppointmentNotesCommand, int>
    {
        public async Task<int> Handle(UpdateAppointmentNotesCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling UpdateAppointmentNotesCommand: Appointment Id:{Id} | Notes:{Notes}", request.Id, request.Notes);

            var appointment = await appointmentRepository.GetByIdAsync(request.Id);
            if (appointment == null)
            {
                logger.LogWarning("Appointment with Id {Id} not found", request.Id);
                throw new KeyNotFoundException("Appointment not found");
            }

            appointment.UpdateNotes(request.Notes);

            var result = await appointmentRepository.SaveAsync(appointment);

            logger.LogInformation("Handled UpdateAppointmentNotesCommand. Update appointment notes with Id: {Id}", result);

            return result;
        }
    }
}
