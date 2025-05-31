using MediatR;

namespace DentRec.Application.CQRS.Appointments.Commands.CompleteAppointment
{
    public class CompleteAppointmentCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
