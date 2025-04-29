using DentRec.Application.DTOs.Report;
using DentRec.Application.Interfaces;
using DentRec.Domain.Entities;
using Gridify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentRec.Application.Services
{
    public class ReportService(IRepository<PatientLog> patientLogRepository, IRepository<Payment> paymentRepository) : IReportService
    {
        public async Task<Paging<GetDailyReportDto>> GetDailyReportAsync(GridifyQuery gridifyQuery)
        {
            if (gridifyQuery.Filter == null)
            {
                var startDate = DateTime.Today.Date.AddDays(-30);
                var endDate = DateTime.Today.Date;
                gridifyQuery.Filter = $"CreatedOn>={startDate},CreatedOn<{endDate.AddDays(1)}";
            }
            var orderBy = string.Empty;
            //Use ordering in the return
            if (gridifyQuery.OrderBy != null)
            {
                orderBy = gridifyQuery.OrderBy;
                gridifyQuery.OrderBy = null;
            }

            var patientLogs = await patientLogRepository.GetPaginatedRecordsAsync(gridifyQuery);
            var payments = await paymentRepository.GetPaginatedRecordsAsync(gridifyQuery);

            var patientLogGroups = patientLogs.Data.GroupBy(x => x.CreatedOn.Date).ToDictionary(x => x.Key);
            var paymentGroups = payments.Data.GroupBy(x => x.CreatedOn.Date).ToDictionary(x => x.Key);

            var allDates = patientLogGroups.Keys
                .Union(paymentGroups.Keys)
                .Distinct()
                .OrderBy(date => date);

            var report = new List<GetDailyReportDto>();

            foreach (var date in allDates)
            {
                patientLogGroups.TryGetValue(date, out var patientLogGroup);
                paymentGroups.TryGetValue(date, out var paymentGroup);

                report.Add(new GetDailyReportDto
                {
                    Date = date,
                    MorningPatientCount = patientLogGroup?.Where(x => x.ProcedureDate.Hour < 12).Select(x => x.PatientId).Distinct().Count() ?? 0,
                    AfternoonPatientCount = patientLogGroup?.Where(x => x.ProcedureDate.Hour >= 12).Select(x => x.PatientId).Distinct().Count() ?? 0,
                    GCashPayment = paymentGroup?.Where(x => x.PaymentMethod.Equals(PaymentMethod.GCash)).Sum(x => x.Amount) ?? 0,
                    CashPayment = paymentGroup?.Where(x => x.PaymentMethod.Equals(PaymentMethod.Cash)).Sum(x => x.Amount) ?? 0,
                    TotalPaymentAmount = paymentGroup?.Sum(x => x.Amount) ?? 0,
                });
            }
            var resultFilter = new GridifyQuery { OrderBy = orderBy };
            var result = report.AsQueryable().Gridify(resultFilter);
            return result;
        }
    }
}

