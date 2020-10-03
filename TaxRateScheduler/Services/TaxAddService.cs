using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxRateScheduler.Enums;
using TaxRateScheduler.Model;
using TaxRateScheduler.Repository;

namespace TaxRateScheduler.Services
{
    public class TaxAddService : ITaxAddService
    {
        private ITaxRateRepository _taxRateRepository;

        public TaxAddService(ITaxRateRepository taxRateRepository)
        {
            _taxRateRepository = taxRateRepository;
        }

        public async Task<TaxRateModel> AddTaxRate(TaxRateModel model)
        {
            TaxRateModel taxRateModel = null;

            try
            {
                ScheduleType stype;

                if (Enum.TryParse<ScheduleType>(model.ScheduleType, out stype))
                {
                    taxRateModel = new TaxRateModel();
                    taxRateModel.MunicipalityName = model.MunicipalityName;
                    taxRateModel.ScheduleType = stype.ToString();
                    taxRateModel.Year = model.Year;
                    taxRateModel.TaxRate = model.TaxRate;
                    taxRateModel.StartDate = model.StartDate;
                    taxRateModel.EndDate = model.EndDate;
                }

                if (taxRateModel != null)
                {
                    bool isexist = false;
                    switch (stype)
                    {
                        case ScheduleType.Daily:
                            isexist = _taxRateRepository.IsExistDaily(taxRateModel);
                            break;
                        case ScheduleType.Weekly:
                            isexist = _taxRateRepository.IsExistDaily(taxRateModel);
                            break;
                        case ScheduleType.Monthly:
                            isexist = _taxRateRepository.IsExistDaily(taxRateModel);
                            break;
                    }

                    if (!isexist)
                        await _taxRateRepository.AddScheduleTaxRate(taxRateModel);
                    else
                        taxRateModel = null;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return taxRateModel;
        }
    }
}
