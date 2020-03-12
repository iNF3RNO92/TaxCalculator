using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.API.Models.Repository
{
    //public interface IDataRepository<TEntity, U> where TEntity : class
    //{
    //    IEnumerable<TEntity> GetAll();
    //    TEntity Get(U id);
    //    long Add(TEntity b);
    //    long Update(U id, TEntity b);
    //    long Delete(U id);
    //}

    public interface ITaxTypeRepository : IDisposable
    {
        ReturnObject GetPayableTax(string postalCode, decimal anualIncome);

        private void Save() { }
    }
}
