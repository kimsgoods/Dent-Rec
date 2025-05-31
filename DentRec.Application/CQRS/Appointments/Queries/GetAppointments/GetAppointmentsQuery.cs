using Gridify;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentRec.Application.CQRS.Appointments.Queries.GetAppointments
{
    public record GetAppointmentsQuery(GridifyQuery GridifyQuery) : IRequest<Paging<GetAppointmentsQueryResult>>;
    
}
