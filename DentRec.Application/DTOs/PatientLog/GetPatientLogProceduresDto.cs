using DentRec.Application.DTOs.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentRec.Application.DTOs.PatientLog
{
    public class GetPatientLogProceduresDto
    {
        public GetProcedureDto Procedure { get; set; } = null!;
        public int Quantity { get; set; } = 1; //Number of teeth involved in this procedure. For PricingType.PerTooth
        public string? Notes { get; set; }
        public decimal AdjustedFee { get; set; }
    }
}
