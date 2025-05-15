using Gridify;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentRec.Application.CQRS.PatientLogs.Queries.GetPatientLogs
{
    public record GetPatientLogsQuery(GridifyQuery GridifyQuery) : IRequest<Paging<GetPatientLogsQueryHandlerResult>>;
}
