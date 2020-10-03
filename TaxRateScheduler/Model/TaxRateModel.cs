using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxRateScheduler.Model
{
    public class TaxRateModel
    {
        [Key, Column(Order = 0)]
        public string MunicipalityName { get; set; }
        [Key, Column(Order = 1)]
        public string ScheduleType { get; set; }

        [Column(TypeName = "decimal(4,2)")]
        public decimal TaxRate { get; set; }

        public int Year { get; set; }

        
        [Key, Column(Order = 2, TypeName ="date")]        
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
    }
}
