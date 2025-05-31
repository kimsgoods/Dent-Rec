using DentRec.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DentRec.Application.CQRS.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommandHandler(IExtendedRepository<Appointment> appointmentRepository,
        IExtendedRepository<Patient> patientRepository,
        IExtendedRepository<Dentist> dentistRepository,
        ILogger<CreateAppointmentCommandHandler> logger) : IRequestHandler<CreateAppointmentCommand, int>
    {
        public async Task<int> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling CreateAppointmentCommand: PatientId:{PatientId}, DentistId:{DentistId}, AppointmentDateTime:{AppointmentDateTime} Notes:{Notes}",
                request.PatientId, request.DentistId, request.AppointmentDateTime, request.Notes);

            var patient = await patientRepository.GetByIdAsync(request.PatientId);
            if (patient is null)
            {
                logger.LogWarning("Patient with Id {PatientId} does not exist", request.PatientId);
                throw new KeyNotFoundException($"Patient with Id {request.PatientId} does not exist.");
            }

            var dentistExists = await dentistRepository.ExistsAsync(request.DentistId);
            if (!dentistExists)
            {
                logger.LogWarning("Dentist with Id {DentistId} does not exist.", request.DentistId);
                throw new KeyNotFoundException($"Dentist with Id {request.DentistId} does not exist.");
            }

            try
            {
                var appointment = new Appointment(request.PatientId, request.DentistId, request.AppointmentDateTime, request.Notes);

                appointmentRepository.Add(appointment);

                var result = await appointmentRepository.SaveAsync(appointment);

                logger.LogInformation("Handled CreateAppointmentCommand. Created new appointment with Id: {Id}", result);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating Appointment for PatientId: {PatientId}", request.PatientId);
                throw new ApplicationException("An error occurred while creating the Appointment.", ex);
            }
        }
    }

}
