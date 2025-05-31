using DentRec.Domain.Entities;
using Gridify;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DentRec.Application.CQRS.Appointments.Queries.GetAppointments
{
    public class GetAppointmentsQueryHandler(IExtendedRepository<Appointment> appointmentRepository,
        ILogger<GetAppointmentsQueryHandler> logger) 
        : IRequestHandler<GetAppointmentsQuery, Paging<GetAppointmentsQueryResult>>
    {

        private readonly Func<IQueryable<Appointment>, IQueryable<Appointment>> includes =
            x => x.Include(p => p.Patient)
                  .Include(p => p.Dentist);
        public async Task<Paging<GetAppointmentsQueryResult>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetAppointmentsQuery with page:{Page}, pageSize:{PageSize}, orderBy:{OrderBy}, filter:{Filter}",
                request.GridifyQuery.Page, request.GridifyQuery.PageSize, request.GridifyQuery.OrderBy, request.GridifyQuery.Filter);

            var appointments = await appointmentRepository.GetPaginatedRecordsAsync(request.GridifyQuery, includes);

            var result = new Paging<GetAppointmentsQueryResult>
            {
                Count = appointments.Count,
                Data = appointments.Data.Select(appointment => new GetAppointmentsQueryResult
                {
                    Id = appointment.Id,
                    PatientId = appointment.Patient.Id,
                    PatientName = $"{appointment.Patient?.FirstName} {appointment.Patient?.LastName}",
                    DentistName = $"{appointment.Dentist?.FirstName} {appointment.Dentist?.LastName}",
                    AppointmentDateTime = appointment.AppointmentDateTime,
                    Notes = appointment.Notes,
                    Status = appointment.Status.ToString()
                })
            };

            logger.LogInformation("Handled GetAppointmentsQuery");
            return result;

        }
    }
    
}
