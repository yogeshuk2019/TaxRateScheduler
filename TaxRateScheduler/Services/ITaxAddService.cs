using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxRateScheduler.Model;

namespace TaxRateScheduler.Services
{
    public interface ITaxAddService
    {
        Task<TaxRateModel> AddTaxRate(TaxRateModel model);
    }
}
