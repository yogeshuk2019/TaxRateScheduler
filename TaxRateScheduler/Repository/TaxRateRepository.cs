using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TaxRateScheduler.Context;
using TaxRateScheduler.Enums;
using TaxRateScheduler.Model;

namespace TaxRateScheduler.Repository
{
    public class TaxRateRepository : ITaxRateRepository
    {
        private readonly MunicipalityTaxRateContext _municipalityTaxRateContext;

        public TaxRateRepository(MunicipalityTaxRateContext municipalityTaxRateContext)
        {
            _municipalityTaxRateContext = municipalityTaxRateContext;
        }

        public async Task AddScheduleBulkTaxRate(List<TaxRateModel> bulkList)
        {
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //_municipalityTaxRateContext.tblTaxRates.Add(bulkList);
                    //_municipalityTaxRateContext.SaveChanges();
                    await _municipalityTaxRateContext.BulkInsertAsync(bulkList, options =>
                    {
                        options.PreserveInsertOrder = true;
                        options.SetOutputIdentity = true;
                    });
                    transaction.Complete();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<TaxRateModel> AddScheduleTaxRate(TaxRateModel taxRateModel)
        {
            try
            {
                _municipalityTaxRateContext.tblTaxRates.Add(taxRateModel);
                await _municipalityTaxRateContext.SaveChangesAsync();
                return taxRateModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> GetTaxRate(string mname, string scheduleType, DateTime taxRateDate)
        {
            decimal taxrate = 0;
            try
            {
                //string dateFormat = taxRateDate.ToString("yyyy-MM-dd");
                var tempResult = await _municipalityTaxRateContext.tblTaxRates
                                                            .FirstOrDefaultAsync(d => d.MunicipalityName.Equals(mname) && d.ScheduleType.Equals(scheduleType)
                                                             && (d.StartDate >= taxRateDate && d.EndDate <= taxRateDate)
                                                             );
                if (tempResult != null)
                    taxrate = tempResult.TaxRate;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return taxrate;
        }

        public List<string> GetScheduleTypes()
        {
            List<string> list = Enum.GetNames(typeof(ScheduleType)).ToList();

            return list;
        }

        public bool IsExistDaily(TaxRateModel model)
        {
            return _municipalityTaxRateContext.tblTaxRates.Any(d => d.MunicipalityName == model.MunicipalityName
            && d.StartDate == model.StartDate && d.ScheduleType == ScheduleType.Daily.ToString());
        }

        public bool IsExistWeekly(TaxRateModel model)
        {
            return _municipalityTaxRateContext.tblTaxRates.Any(d => d.MunicipalityName == model.MunicipalityName
            && d.StartDate >= model.StartDate && d.EndDate <= model.EndDate && d.ScheduleType == ScheduleType.Weekly.ToString());
        }

        public bool IsExistMonthly(TaxRateModel model)
        {
            return _municipalityTaxRateContext.tblTaxRates.Any(d => d.MunicipalityName == model.MunicipalityName
            && d.StartDate >= model.StartDate && d.EndDate <= model.EndDate && d.ScheduleType == ScheduleType.Monthly.ToString());
        }
    }
}
