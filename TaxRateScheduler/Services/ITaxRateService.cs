using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxRateScheduler.Model;

namespace TaxRateScheduler.Services
{
    public interface ITaxRateService
    {
        Task<decimal> GetTaxRate(string name, DateTime dateTime);

    }
}
