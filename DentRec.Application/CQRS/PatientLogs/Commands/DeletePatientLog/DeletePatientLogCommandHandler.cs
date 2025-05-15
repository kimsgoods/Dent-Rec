using DentRec.Application.CRUD.Interfaces;
using DentRec.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DentRec.Application.CQRS.PatientLogs.Commands.DeletePatientLog
{
    public class DeletePatientLogCommandHandler(IExtendedRepository<PatientLog> repository, ILogger<DeletePatientLogCommandHandler> logger) : IRequestHandler<DeletePatientLogCommand, bool>
    {
        public async Task<bool> Handle(DeletePatientLogCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling DeletePatientLogCommand for PatientLog Id: {PatientLogId}", request.Id);

            try
            {
                var patientLog = await repository.GetByIdAsync(request.Id);
                if (patientLog == null)
                {
                    logger.LogWarning("PatientLog with Id: {PatientLogId} not found", request.Id);
                    throw new KeyNotFoundException($"Could not find PatientLog with Id: {request.Id}");
                }

                repository.Remove(patientLog);
                var result = await repository.SaveAsync(patientLog) > 0;

                if (result)
                {
                    logger.LogInformation("Successfully deleted PatientLog with Id: {PatientLogId}", request.Id);
                }
                else
                {
                    logger.LogWarning("Failed to delete PatientLog with Id: {PatientLogId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deleting PatientLog with Id: {PatientLogId}", request.Id);
                throw new ApplicationException("An error occurred while deleting the PatientLog.", ex);
            }
        }
    }
}
