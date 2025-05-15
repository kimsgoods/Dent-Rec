using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentRec.Application.CQRS.PatientLogs.Commands.UpdatePatientLog
{
    public record UpdatePatientLogCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public int? DentistId { get; set; }
        public DateTime? ProcedureDate { get; set; }
        public string? Notes { get; set; }
    }
}
