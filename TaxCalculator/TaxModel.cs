using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.API.Models
{
    [Table("TaxType")]
    public class TaxType
    {
        public int Id { get; set; }
        public string TaxTypeDescription { get; set; }
    }

    [Table("TaxTypePerPostalCode")]
    public class TaxTypePerPostalCode
    {
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public int TaxTypeId { get; set; }

    }

    [Table("ProgressiveTaxTable")]
    public class ProgressiveTaxTable
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public decimal From { get; set; }
        public decimal To { get; set; }
    }

    [Table("UserPayableTax")]
    public class UserPayableTax
    {
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public decimal AnualIncome { get; set; }
        public decimal PayableTax { get; set; }
        public DateTime DateGenerated { get; set; }
    }

    public class ReturnObject
    {
        public decimal PayableTax { get; set; }
        public string ErrorResult { get; set; }
    }
}
