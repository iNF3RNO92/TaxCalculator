using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.API.Models;

namespace TaxCalculator
{
    public class TaxManager
    {
        private TaxCalculatorBase Calculator { get; set; }
        public List<ProgressiveTaxTable> ProgressiveTaxTables { get; set; }

        public TaxManager(TaxCalculationType taxType, decimal anualIncome)
        {            
            switch (taxType)
            {
                case TaxCalculationType.Progressive:
                    Calculator = new ProgressiveTax();                    
                    break;
                case TaxCalculationType.FlatValue:
                    Calculator = new FlatValueTax();
                    break;
                case TaxCalculationType.FlatRate:
                    Calculator = new FlatValueTax();
                    break;
                case TaxCalculationType.None:
                    throw new ArgumentOutOfRangeException("PostalCode", "Postal Code could not be matched against a tax type");
            }

            Calculator.TotalAmount = anualIncome;
        }

        public decimal CalculateTax()
        {
            Calculator.ProgressiveTaxTables = ProgressiveTaxTables;
            return Calculator.CalculateTax();
        }
    }

    public enum TaxCalculationType
    {
        None = 0,
        Progressive = 1,
        FlatValue = 2,
        FlatRate = 3
    }

    internal abstract class TaxCalculatorBase
    {
        public decimal TotalAmount { get; set; }
        public abstract decimal CalculateTax();
        internal List<ProgressiveTaxTable> ProgressiveTaxTables { get; set; }
    }

    internal class FlatValueTax : TaxCalculatorBase
    {
        public override decimal CalculateTax()
        {
            return TotalAmount * 0.175m;
        }
    }

    internal class FlatRateTax : TaxCalculatorBase
    {
        public override decimal CalculateTax()
        {
            return TotalAmount > 200000 ? TotalAmount * 0.05m : 10000;
        }
    }

    internal class ProgressiveTax : TaxCalculatorBase
    {        
        public override decimal CalculateTax()
        {                    
            if(ProgressiveTaxTables == null)            
                throw new ArgumentNullException("Progressive Tax Tables", "Progressive Tax Tables have not been loaded or are empty, please contact support");
            
            var taxTableArray = ProgressiveTaxTables.OrderBy(x => x.From).ToArray();

            decimal taxableAmount = 0;

            for (int i = 0; i < taxTableArray.Length; i++)
            {
                if (i == taxTableArray.Length)
                {
                    taxableAmount += TotalAmount - taxTableArray[i].To > 0 ? (TotalAmount - taxTableArray[i].To) * taxTableArray[i].Rate : 0;
                    break;
                }
                taxableAmount += TotalAmount - taxTableArray[i].To > 0 ? Math.Min(TotalAmount - taxTableArray[i].To, taxTableArray[i + 1].To - taxTableArray[i].To) * taxTableArray[i].Rate : 0;
            }

            return taxableAmount;
        }        
    }

}
