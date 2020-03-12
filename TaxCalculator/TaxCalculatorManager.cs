using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.API.Models.Repository;
using TaxCalculator.API.Models;

namespace TaxCalculator.API.Models.DataManager
{

    public class TaxTypeRepository : ITaxTypeRepository
    {
        private readonly ApplicationContext _context;

        public TaxTypeRepository(ApplicationContext context)
        {
            _context = context;
        }        

        public List<ProgressiveTaxTable> GetTaxBrackets()
        {
            return _context.ProgressiveTaxTables.ToList();
        }

        public ReturnObject GetPayableTax(string postalCode, decimal anualIncome)
        {
            ReturnObject result = new ReturnObject();

            var calculationType = GetTaxCalculationType(postalCode);

            try
            {
                TaxManager taxManager = new TaxManager(calculationType, anualIncome);

                if (calculationType == TaxCalculationType.Progressive)
                    taxManager.ProgressiveTaxTables = _context.ProgressiveTaxTables.ToList();

                result.PayableTax = taxManager.CalculateTax();
            }
            catch (Exception ex)
            {
                result.ErrorResult = ex.Message;
            }

            Save(postalCode, anualIncome, result.PayableTax);

            return result;
        }

        private void Save(string postalCode, decimal anualIncome, decimal payableTax)
        {
            var newUserPayableTax = new UserPayableTax()
            {
                AnualIncome = anualIncome,
                DateGenerated = DateTime.Now,
                PayableTax = payableTax,
                PostalCode = postalCode
            };

            _context.UserPayableTaxes.Add(newUserPayableTax);
            _context.SaveChanges();
        }

        private TaxCalculationType GetTaxCalculationType(string PostalCode)
        {
            var calculationType = _context.TaxTypePerPostalCode.Where(x => x.PostalCode == PostalCode).SingleOrDefault();
            return calculationType == null ? TaxCalculationType.None : (TaxCalculationType)calculationType.TaxTypeId;            
        }

        public void Dispose()
        {

        }
    }
}
