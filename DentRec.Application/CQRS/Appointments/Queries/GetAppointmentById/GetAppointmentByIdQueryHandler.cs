using DentRec.Application.CQRS.Appointments.Commands.UpdateAppointment;
using DentRec.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DentRec.Application.CQRS.Appointments.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQueryHandler(IExtendedRepository<Appointment> appointmentRepository,
        ILogger<UpdateAppointmentNotesCommand> logger) : IRequestHandler<GetAppointmentByIdQuery, GetAppointmentByIdQueryResult>
    {

        private readonly Func<IQueryable<Appointment>, IQueryable<Appointment>> includes =
            x => x.Include(p => p.Patient)
                  .Include(p => p.Dentist);

        public async Task<GetAppointmentByIdQueryResult> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetAppointmentByIdQuery: Appointment Id:{Id}", request.Id);

            var appointment = await appointmentRepository.GetByIdAsync(request.Id, includes);
            if (appointment == null)
            {
                logger.LogWarning("Appointment with Id {Id} not found", request.Id);
                throw new KeyNotFoundException("Appointment not found");
            }

            var result = new GetAppointmentByIdQueryResult
            {
                Id = appointment.Id,
                PatientId = appointment.Patient.Id,
                PatientName = $"{appointment.Patient?.FirstName} {appointment.Patient?.LastName}",
                Gender = appointment.Patient?.Gender ?? string.Empty,
                Age = appointment.Patient?.Age ?? 0,
                AppointmentDateTime = appointment.AppointmentDateTime,
                Address = appointment.Patient?.Address ?? string.Empty,
                DentistId = appointment.Dentist.Id,
                DentistName = $"{appointment.Dentist?.FirstName} {appointment.Dentist?.LastName}",
                Status = appointment.Status.ToString(),
                Notes = appointment.Notes
            };            

            logger.LogInformation("Handled GetAppointmentByIdQuery");

            return result;
        }
    }

}
