using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculatorWeb
{
    public class TaxRequestModel
    {
        [Required(ErrorMessage = "Postal Code is required")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Anual Salary is required")]
        [Display(Name = "Anual Salary")]
        public decimal AnualSalary { get; set; }

        [DisplayFormat(DataFormatString ="0.00")]
        public decimal Result { get; set; }

    }
}
