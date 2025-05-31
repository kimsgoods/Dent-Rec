using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentRec.Application.CQRS.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommand : IRequest<int>
    {
        public int PatientId { get; set; }
        public int DentistId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string? Notes { get; set; }
    }

}
