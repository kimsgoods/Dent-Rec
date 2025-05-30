using MediatR;

namespace DentRec.Application.CQRS.PatientLogs.Commands.CreatePatientLog
{
    public record CreatePatientLogCommand : IRequest<int>
    {
        public required int PatientId { get; set; }
        public required int DentistId { get; set; }
        public List<CreatePatientLogProcedureCommand> Procedures { get; set; } = [];
        public string? Notes { get; set; }
    }

    public record CreatePatientLogProcedureCommand
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public string? Notes { get; set; }

    }
}

