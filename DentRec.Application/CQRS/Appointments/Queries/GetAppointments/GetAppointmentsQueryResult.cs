namespace DentRec.Application.CQRS.Appointments.Queries.GetAppointments
{
    public class GetAppointmentsQueryResult
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;   
        public string DentistName { get; set; } = string.Empty;
        public DateTime AppointmentDateTime { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = string.Empty;
    }
    
}
