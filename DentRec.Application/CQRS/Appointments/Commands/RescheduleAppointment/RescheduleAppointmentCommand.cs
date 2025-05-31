using MediatR;

namespace DentRec.Application.CQRS.Appointments.Commands.RescheduleAppointment
{
    public class RescheduleAppointmentCommand : IRequest<int>
    {
        public required int Id { get; set; }
        public required DateTime NewAppointmentDateTime { get; set; }
    }
}
