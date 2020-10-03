using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxRateScheduler.Enums;
using TaxRateScheduler.Model;

namespace TaxRateScheduler.Repository
{
    public interface ITaxRateRepository
    {
        Task<TaxRateModel> AddScheduleTaxRate(TaxRateModel taxRateModel);

        Task AddScheduleBulkTaxRate(List<TaxRateModel> bulkList);

        Task<decimal> GetTaxRate(string mname, string scheduleType, DateTime taxRateDate);

        List<string> GetScheduleTypes();

        bool IsExistDaily(TaxRateModel model);

        bool IsExistWeekly(TaxRateModel model);

        bool IsExistMonthly(TaxRateModel model);
    }
}
