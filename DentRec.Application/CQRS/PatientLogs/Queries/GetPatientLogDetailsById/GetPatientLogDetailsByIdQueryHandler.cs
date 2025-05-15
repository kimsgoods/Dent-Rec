using DentRec.Application.CRUD.Interfaces;
using DentRec.Application.Extensions;
using DentRec.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DentRec.Application.CQRS.PatientLogs.Queries.GetPatientLogDetailsById
{
    public class GetPatientLogDetailsByIdQueryHandler(IExtendedRepository<PatientLog> repository, ILogger<GetPatientLogDetailsByIdQueryHandler> logger)
        : IRequestHandler<GetPatientLogDetailsByIdQuery, GetPatientLogDetailsByIdQueryResult>
    {
        private readonly Func<IQueryable<PatientLog>, IQueryable<PatientLog>> includes =
            x => x.Include(p => p.Patient)
                  .Include(p => p.Dentist)
                  .Include(p => p.PatientLogProcedures)
                    .ThenInclude(plp => plp.Procedure)
                  .Include(p => p.Payments);
        public async Task<GetPatientLogDetailsByIdQueryResult> Handle(GetPatientLogDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetPatientLogDetailsByIdQuery for PatientLog Id: {PatientLogId}", request.Id);

            try
            {
                var patientLog = await repository.GetByIdAsync(request.Id, includes);
                if (patientLog is null)
                {
                    logger.LogError("Could not find PatientLog with Id:{PatientLogId}", request.Id);
                    throw new KeyNotFoundException($"Could not find PatientLog with Id: {request.Id}");
                }

                var result = new GetPatientLogDetailsByIdQueryResult
                {
                    Id = patientLog.Id,
                    DentistId = patientLog.DentistId,
                    DentistName = $"{patientLog.Dentist?.FirstName} {patientLog.Dentist?.LastName}".Trim(),
                    PatientId = patientLog.PatientId,
                    PatientName = $"{patientLog.Patient?.FirstName} {patientLog.Patient?.LastName}".Trim(),
                    Gender = patientLog.Patient?.Gender ?? string.Empty,
                    Age = patientLog.PatientAge,
                    Address = patientLog.Patient?.Address ?? string.Empty,
                    ProcedureDate = patientLog.ProcedureDate,
                    Fee = patientLog.Fee,
                    PaymentStatus = patientLog.PaymentStatus,
                    Notes = patientLog.Notes,
                    Procedures = patientLog.PatientLogProcedures.Select(x => x.ToDto()),
                    CreatedBy = patientLog.CreatedBy,
                    ModifiedOn = patientLog.ModifiedOn,
                    CreatedOn = patientLog.CreatedOn,
                    ModifiedBy = patientLog.ModifiedBy,
                    Payments = patientLog.Payments.Select(x => x.ToDto())
                };

                logger.LogInformation("Successfully retrieved PatientLog details for Id: {PatientLogId}", request.Id);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving PatientLog details for Id: {PatientLogId}", request.Id);
                throw;
            }

        }
    }
}
