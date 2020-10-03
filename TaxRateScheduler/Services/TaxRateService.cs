using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxRateScheduler.Enums;
using TaxRateScheduler.Model;
using TaxRateScheduler.Repository;

namespace TaxRateScheduler.Services
{
    public class TaxRateService : ITaxRateService
    {

        private ITaxRateRepository _taxRateRepository;

        public TaxRateService(ITaxRateRepository taxRateRepository)
        {
            _taxRateRepository = taxRateRepository;
        }

        public async Task<decimal> GetTaxRate(string name, DateTime dateTime)
        {
            decimal taxrate = 0;
            try
            {
                taxrate = await _taxRateRepository.GetTaxRate(name, ScheduleType.Daily.ToString(), dateTime);
                if (taxrate == 0)
                {
                    taxrate = await _taxRateRepository.GetTaxRate(name, ScheduleType.Weekly.ToString(), dateTime);
                    if (taxrate == 0)
                        taxrate = await _taxRateRepository.GetTaxRate(name, ScheduleType.Monthly.ToString(), dateTime);
                }

                if (taxrate == 0)
                    taxrate = await _taxRateRepository.GetTaxRate(name, ScheduleType.Yearly.ToString(), dateTime);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return taxrate;
        }
    }
}
