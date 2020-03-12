using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaxCalculator.API.Models;
using TaxCalculator.API.Models.DataManager;
using TaxCalculator.API.Models.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaxCalculator
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : Controller
    {

        public ITaxTypeRepository _repository;

        public TaxController(ITaxTypeRepository repository)
        {
            _repository = repository;
        }      

        [HttpGet]
        public string Get()
        {
            return "Welcome to the Tax Service";
        }

        [HttpGet("{postalCode}/{anualIncome}")]
        public ReturnObject Get(string postalCode, decimal anualIncome)
        {
            return _repository.GetPayableTax(postalCode, anualIncome);
        }
    }
}
