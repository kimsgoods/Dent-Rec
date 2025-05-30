using Gridify;
using MediatR;

namespace DentRec.Application.CQRS.PatientLogs.Queries.GetPatientLogs
{
    public record GetPatientLogsQuery(GridifyQuery GridifyQuery) : IRequest<Paging<GetPatientLogsQueryHandlerResult>>;
}
