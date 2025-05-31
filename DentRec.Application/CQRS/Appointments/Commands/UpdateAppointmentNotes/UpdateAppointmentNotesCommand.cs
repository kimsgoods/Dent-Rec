using MediatR;

namespace DentRec.Application.CQRS.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentNotesCommand : IRequest<int>
    {
        public required int Id { get; set; }
        public required string Notes { get; set; }
    }
}
