using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TaxCalculatorWeb.Web.Helper;

namespace TaxCalculatorWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public TaxRequestModel TaxRequestModel { get; set; }

        public IActionResult OnGet()
        {
            TaxRequestModel ??= new TaxRequestModel();

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {            
            if (ModelState.IsValid)
            {
                Helper serviceHelper = new Helper();
                var result = await serviceHelper.GetPayableTax(TaxRequestModel.PostalCode, TaxRequestModel.AnualSalary);

                if (!string.IsNullOrEmpty(result.ErrorResult))
                    ModelState.AddModelError("", result.ErrorResult);
                else
                    TaxRequestModel.Result = result.PayableTax;               
            }

            return Page();
        }
    }
}
