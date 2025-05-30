using MediatR;

namespace DentRec.Application.CQRS.PatientLogs.Queries.GetPatientLogDetailsById
{
    public record GetPatientLogDetailsByIdQuery(int Id) : IRequest<GetPatientLogDetailsByIdQueryResult>;
}
