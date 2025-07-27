using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace GIBS.Module.TaxRates.Models
{
    [Table("GIBSTaxRates")]
    public class TaxRates : IAuditable
    {
        [Key]
        public int TaxRatesId { get; set; }
        public int ModuleId { get; set; }
        public string Municipality { get; set; }
        public string Fiscal_Year { get; set; }
        public string DOR_Code { get; set; }
        public decimal Residential { get; set; }
        public decimal Open_Space { get; set; }
        public decimal Commercial { get; set; }
        public decimal Industrial { get; set; }
        public decimal Personal_Property { get; set; }


        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
