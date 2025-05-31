using DentRec.Domain.Enums;

namespace DentRec.Application.CQRS.Appointments.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQueryResult
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
        public int DentistId { get; set; }
        public string DentistName { get; set; } = string.Empty;
        public DateTime AppointmentDateTime { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = string.Empty;

    }

}
