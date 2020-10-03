using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxRateScheduler.Model;

namespace TaxRateScheduler.Services
{
    public interface IFileProcessService
    {
        Task ProcessFile(IFormFile file);
    }
}
