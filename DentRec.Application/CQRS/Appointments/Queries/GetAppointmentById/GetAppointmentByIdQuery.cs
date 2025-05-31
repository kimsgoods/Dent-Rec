using DentRec.Application.CRUD.DTOs.Dentist;
using DentRec.Application.CRUD.DTOs.Patient;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentRec.Application.CQRS.Appointments.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQuery : IRequest<GetAppointmentByIdQueryResult>
    {
        public int Id { get; set; }
    }

}
