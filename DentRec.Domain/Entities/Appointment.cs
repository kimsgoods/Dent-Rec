using DentRec.Domain.Enums;

namespace DentRec.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public int PatientId { get; private set; }
        public int DentistId { get; private set; }
        public DateTime AppointmentDateTime { get; private set; }
        public string? Notes { get; private set; }
        public AppointmentStatus Status { get; private set; }

        public Patient Patient { get; set; } = null!;


        public Appointment() { }
        public Appointment(int patientId, int dentistId, DateTime appointmentDateTime, string? notes)
        {
            if (appointmentDateTime < DateTime.UtcNow)
                throw new ArgumentException("Appointment cannot be in the past.");

            PatientId = patientId;
            DentistId = dentistId;
            AppointmentDateTime = appointmentDateTime;
            Notes = notes;
            Status = AppointmentStatus.Scheduled;
        }

        public void UpdateNotes(string notes)
        {
            Notes = notes;
        }

        public void Cancel()
        {
            Status = AppointmentStatus.Cancelled;
        }

        public void Complete()
        {
            Status = AppointmentStatus.Completed;
        }

        public void Reschedule(DateTime newDateTime)
        {
            if (newDateTime < DateTime.UtcNow)
                throw new ArgumentException("Cannot reschedule to a past date.");

            AppointmentDateTime = newDateTime;
            Status = AppointmentStatus.Scheduled;
        }
    }
}

