using MediatR;

namespace DentRec.Application.CQRS.Appointments.Commands.CancelAppointment
{
    public class CancelAppointmentCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
