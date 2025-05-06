namespace DentRec.Application.DTOs.Report
{
    public class GetDailyReportDto
    {
        public DateTime Date { get; set; }
        public decimal CashPayment { get; set; }
        public decimal GCashPayment { get; set; }
        public decimal TotalPaymentAmount { get; set; }
        public int MorningPatientCount { get; set; }
        public int AfternoonPatientCount { get; set; }
        public int DailyPatientCount => MorningPatientCount + AfternoonPatientCount;
    }
}
