using DentRec.Domain.Enums;

namespace DentRec.Domain.Entities
{
    public class PatientLogProcedure : BaseEntity
    {
        public int PatientLogId { get; set; }
        public int ProcedureId { get; set; }
        public int Quantity { get; set; } = 1; //Number of teeth involved in this procedure. For PricingType.PerTooth
        public string? Notes { get; set; }
        public decimal AdjustedFee { get; private set; }

        public PatientLog PatientLog { get; set; } = null!;
        public Procedure Procedure { get; set; } = null!;

        public void CalculateAdjustedFee()
        {
            AdjustedFee = Procedure.PricingType == PricingType.Fixed ? Procedure.Fee : Procedure.Fee * Quantity;
        }
    }
}
