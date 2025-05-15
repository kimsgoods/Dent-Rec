using DentRec.Application.CRUD.Interfaces;
using DentRec.Domain.Entities;
using Gridify;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DentRec.Application.CQRS.PatientLogs.Queries.GetPatientLogs
{
    public class GetPatientLogsQueryHandler(IExtendedRepository<PatientLog> repository, ILogger<GetPatientLogsQueryHandler> logger)
            : IRequestHandler<GetPatientLogsQuery, Paging<GetPatientLogsQueryHandlerResult>>
    {

        private readonly Func<IQueryable<PatientLog>, IQueryable<PatientLog>> includes =
            x => x.Include(p => p.Patient)
                  .Include(p => p.Dentist)
                  .Include(p => p.PatientLogProcedures)
                    .ThenInclude(plp => plp.Procedure)
                  .Include(p => p.Payments);
        public async Task<Paging<GetPatientLogsQueryHandlerResult>> Handle(GetPatientLogsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetPatientLogsQuery with page:{Page}, pageSize:{PageSize}, orderBy:{OrderBy}, filter:{Filter}",
                request.GridifyQuery.Page, request.GridifyQuery.PageSize, request.GridifyQuery.OrderBy, request.GridifyQuery.Filter);

            try
            {
                var patientLogs = await repository.GetPaginatedRecordsAsync(request.GridifyQuery, includes);
                var result = new Paging<GetPatientLogsQueryHandlerResult>
                {
                    Count = patientLogs.Count,
                    Data = patientLogs.Data.Select(patientLog => new GetPatientLogsQueryHandlerResult
                    {
                        Id = patientLog.Id,
                        PatientId = patientLog.PatientId,
                        PatientName = $"{patientLog.Patient?.FirstName} {patientLog.Patient?.LastName}".Trim(),
                        Gender = patientLog.Patient?.Gender ?? string.Empty,
                        Age = patientLog.PatientAge,
                        Address = patientLog.Patient?.Address ?? string.Empty,
                        ProcedureDate = patientLog.ProcedureDate,
                        Fee = patientLog.Fee,
                        Notes = patientLog.Notes,
                        Procedures = string.Join(", ", patientLog.PatientLogProcedures.Select(x => x.Procedure!.Name)),
                        PaymentStatus = patientLog.PaymentStatus
                    })
                };

                logger.LogInformation("Successfully retrieved {Count} patient logs.", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while retrieving patient logs.");
                throw;
            }
        }
    }
}
