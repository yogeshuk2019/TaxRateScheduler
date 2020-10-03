using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxRateScheduler.Context;
using TaxRateScheduler.Enums;
using TaxRateScheduler.Model;
using TaxRateScheduler.Repository;
using TaxRateScheduler.Services;

namespace TaxRateScheduler.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ScheduleTaxRateController : ControllerBase
    {
        private readonly IFileProcessService _fileProcessService;
        private readonly ITaxAddService _taxAddService;
        private readonly ITaxRateService _taxRateService;
        private readonly ITaxRateRepository _taxRateRepository;
        public ScheduleTaxRateController(IFileProcessService fileProcessService,
            ITaxAddService taxAddService,
            ITaxRateService taxRateService,
            ITaxRateRepository taxRateRepository)
        {
            _fileProcessService = fileProcessService;
            _taxAddService = taxAddService;
            _taxRateService = taxRateService;
            _taxRateRepository = taxRateRepository;

        }

        [HttpGet("{mname}/{date}")]
        public async Task<ActionResult<decimal>> GetTaxRate(string mname, DateTime date)
        {
            decimal taxrate = 0;
            try
            {
                if (string.IsNullOrEmpty(mname) || date == null)
                    return BadRequest(new { message = "Invalid inputs" });

                taxrate = await _taxRateService.GetTaxRate(mname, date);

                if (taxrate == 0)
                {
                    return NotFound();
                }
            }

            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }

            return taxrate;
        }

        [HttpPost]
        public async Task<IActionResult> UploadTaxRateFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");

                if (CheckIfTextFile(file))
                {
                    await _fileProcessService.ProcessFile(file);
                }
                else
                {
                    return BadRequest(new { message = "Invalid file extension" });
                }
            }

            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }

            return Ok("File uploaded successfully");
        }


        private bool CheckIfTextFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".txt");
        }

        [HttpPost]
        public async Task<ActionResult<TaxRateModel>> AddTaxRate(TaxRateModel taxRateModel)
        {
            try
            {
                var result = await _taxRateRepository.AddScheduleTaxRate(taxRateModel);

                if (result == null)
                    return new ObjectResult("Either Record already exists or input values are wrong");
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }

            return Ok(taxRateModel);
        }

        [HttpGet]
        public IActionResult GetScheduleType()
        {
            var type = _taxRateRepository.GetScheduleTypes();
            if (type == null)
            {
                return NotFound();
            }
            return Ok(type);
        }
    }
}
