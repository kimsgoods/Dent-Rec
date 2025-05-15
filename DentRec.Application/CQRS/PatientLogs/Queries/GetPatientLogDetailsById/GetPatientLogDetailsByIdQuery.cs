using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentRec.Application.CQRS.PatientLogs.Queries.GetPatientLogDetailsById
{
    public record GetPatientLogDetailsByIdQuery(int Id) : IRequest<GetPatientLogDetailsByIdQueryResult>;
}
