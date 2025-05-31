using MediatR;

namespace DentRec.Application.CQRS.PatientLogs.Commands.DeletePatientLog
{
    public record DeletePatientLogCommand(int Id) : IRequest<bool>;
}
