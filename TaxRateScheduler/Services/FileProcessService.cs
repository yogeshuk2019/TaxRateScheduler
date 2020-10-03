using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxRateScheduler.Enums;
using TaxRateScheduler.Model;
using TaxRateScheduler.Repository;

namespace TaxRateScheduler.Services
{
    public class FileProcessService : IFileProcessService
    {
        private ITaxRateRepository _taxRateRepository;

        public FileProcessService(ITaxRateRepository taxRateRepository)
        {
            _taxRateRepository = taxRateRepository;
        }

        public async Task ProcessFile(IFormFile file)
        {
            var result = new StringBuilder();
            string date = string.Empty;
            List<TaxRateModel> taxrateValues = new List<TaxRateModel>();
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    string headerLine = reader.ReadLine();
                    string line;
                    string stype = ScheduleType.Yearly.ToString();
                    while ((line = reader.ReadLine()) != null)
                    {
                        var splitValue = line.Split("|");
                        var taxrate = Convert.ToDecimal(splitValue[1]);
                        var mname = splitValue[0];
                        var year = Convert.ToInt32(splitValue[2]);
                        DateTime firstDay = new DateTime(year, 1, 1);
                        DateTime lastDay = new DateTime(year, 12, 31);
                        taxrateValues.Add(new TaxRateModel()
                        {
                            MunicipalityName = mname,
                            TaxRate = taxrate,
                            ScheduleType = stype,
                            Year = year,
                            StartDate = firstDay,
                            EndDate = lastDay
                        }); ;
                    }
                }

                if (taxrateValues.Count > 0)
                    await _taxRateRepository.AddScheduleBulkTaxRate(taxrateValues);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
