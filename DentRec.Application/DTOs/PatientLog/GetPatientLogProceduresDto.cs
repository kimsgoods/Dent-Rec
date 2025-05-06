using DentRec.Application.DTOs.Procedure;

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
