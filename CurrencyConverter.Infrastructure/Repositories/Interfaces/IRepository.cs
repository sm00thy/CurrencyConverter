using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyConverter.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAvailableCurrencies();
        Task AddAsync(T entity);
    }
}