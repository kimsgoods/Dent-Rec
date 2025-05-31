using DentRec.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DentRec.Application.CQRS.Appointments.Commands.CompleteAppointment
{
    public class CompleteAppointmentCommandHandler(IExtendedRepository<Appointment> appointmentRepository,
        ILogger<CompleteAppointmentCommandHandler> logger) : IRequestHandler<CompleteAppointmentCommand, int>
    {
        public async Task<int> Handle(CompleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling CompleteAppointmentCommand: Appointment Id:{Id}", request.Id);

            var appointment = await appointmentRepository.GetByIdAsync(request.Id);
            if (appointment == null)
            {
                logger.LogWarning("Appointment with Id {Id} not found", request.Id);
                throw new KeyNotFoundException("Appointment not found");
            }

            appointment.Complete();

            var result = await appointmentRepository.SaveAsync(appointment);

            logger.LogInformation("Handled CompleteAppointmentCommand. Completeled appointment with Id: {Id}", result);

            return result;

        }
    }
}
