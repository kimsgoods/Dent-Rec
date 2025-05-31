using DentRec.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DentRec.Application.CQRS.Appointments.Commands.CancelAppointment
{
    public class CancelAppointmentCommandHandler(IExtendedRepository<Appointment> appointmentRepository,
        ILogger<CancelAppointmentCommandHandler> logger) : IRequestHandler<CancelAppointmentCommand, int>
    {
        public async Task<int> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling CancelAppointmentCommand: Appointment Id:{Id}", request.Id);

            var appointment = await appointmentRepository.GetByIdAsync(request.Id);
            if (appointment == null)
            {
                logger.LogWarning("Appointment with Id {Id} not found", request.Id);
                throw new KeyNotFoundException("Appointment not found");
            }

            appointment.Cancel();

            var result = await appointmentRepository.SaveAsync(appointment);

            logger.LogInformation("Handled CancelAppointmentCommand. Cancelled appointment with Id: {Id}", result);

            return result;

        }
    }
}
