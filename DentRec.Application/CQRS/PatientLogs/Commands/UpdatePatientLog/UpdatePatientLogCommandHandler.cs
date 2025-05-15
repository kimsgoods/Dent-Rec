using DentRec.Application.CQRS.PatientLogs.Commands.CreatePatientLog;
using DentRec.Application.CRUD.Interfaces;
using DentRec.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DentRec.Application.CQRS.PatientLogs.Commands.UpdatePatientLog
{
    public class UpdatePatientLogCommandHandler(
        IExtendedRepository<PatientLog> patientLogRepository,
        IExtendedRepository<Patient> patientRepository,
        IExtendedRepository<Dentist> dentistRepository,
        ILogger<CreatePatientLogCommandHandler> logger) : IRequestHandler<UpdatePatientLogCommand, int>
    {
        public async Task<int> Handle(UpdatePatientLogCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling UpdatePatientLogCommand with PatientLogId: {PatientLogId}", request.Id);

            var patientLog = await patientLogRepository.GetByIdAsync(request.Id);
            if (patientLog is null)
            {
                logger.LogWarning("PatientLog with Id: {PatientLogId} not found", request.Id);
                throw new KeyNotFoundException($"Could not find PatientLog with Id: {request.Id}");
            }

            if (request.PatientId.HasValue)
            {
                var patientExists = await patientRepository.ExistsAsync(request.PatientId.Value);
                if (!patientExists)
                {
                    logger.LogError("Patient with Id: {PatientId} does not exist", request.PatientId.Value);
                    throw new KeyNotFoundException($"Patient with Id {request.PatientId.Value} does not exist.");
                }
                patientLog.PatientId = request.PatientId.Value;
            }
            if (request.DentistId.HasValue)
            {
                var dentistExists = await dentistRepository.ExistsAsync(request.DentistId.Value);
                if (!dentistExists)
                {
                    logger.LogError("Dentist with Id: {DentistId} does not exist", request.DentistId.Value);
                    throw new KeyNotFoundException($"Dentist with Id {request.DentistId.Value} does not exist.");
                }
                patientLog.DentistId = request.DentistId.Value;
            }

            if (!string.IsNullOrEmpty(request.Notes)) patientLog.Notes = request.Notes;
            if (request.ProcedureDate.HasValue) patientLog.ProcedureDate = request.ProcedureDate.Value;

            try
            {
                patientLogRepository.Update(patientLog);
                var result = await patientLogRepository.SaveAsync(patientLog);

                logger.LogInformation("Successfully updated PatientLog with Id: {PatientLogId}", patientLog.Id);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while saving the PatientLog with Id: {PatientLogId}", patientLog.Id);
                throw new ApplicationException("An error occurred while saving the PatientLog.", ex);
            }
        }
    }
}
