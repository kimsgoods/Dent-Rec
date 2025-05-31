using DentRec.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DentRec.Application.CQRS.Appointments.Commands.RescheduleAppointment
{
    public class RescheduleAppointmentCommandHandler(IExtendedRepository<Appointment> appointmentRepository,
        ILogger<RescheduleAppointmentCommand> logger) : IRequestHandler<RescheduleAppointmentCommand, int>
    {
        public async Task<int> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling RescheduleAppointmentCommand: Appointment Id:{Id} | NewAppointmentDateTime:{NewAppointmentDateTime}", request.Id, request.NewAppointmentDateTime);

            var appointment = await appointmentRepository.GetByIdAsync(request.Id);
            if (appointment == null)
            {
                logger.LogWarning("Appointment with Id {Id} not found", request.Id);
                throw new KeyNotFoundException("Appointment not found");
            }

            appointment.Reschedule(request.NewAppointmentDateTime);

            var result = await appointmentRepository.SaveAsync(appointment);

            logger.LogInformation("Handled RescheduleAppointmentCommand. Updated appointment with Id: {Id}", result);

            return result;
        }
    }
}
