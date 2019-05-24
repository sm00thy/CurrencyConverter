using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyConverter.Core.Entities;
using CurrencyConverter.Infrastructure.Data;
using CurrencyConverter.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAvailableCurrencies()
            => await _dbContext.Set<T>().ToListAsync();

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}