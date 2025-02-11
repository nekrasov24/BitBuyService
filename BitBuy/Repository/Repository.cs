using BitBuy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BitBuy.Repository
{
    public class Repository<T, Tkey> : IRepository<T, Tkey> where T : class where Tkey : struct
    {
        private readonly Models.AppContext _appContext;
        private readonly DbSet<T> _dbSet;

        public Repository(Models.AppContext appContext)
        {
            _appContext = appContext;
            _dbSet = _appContext.Set<T>();
        }

        public async Task AddAsync(T obj)
        {
            _dbSet.Add(obj);
            await SaveChangesAsync();
        }

        public async Task EditUser(T obj)
        {
            _dbSet.Update(obj);
            await SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _appContext.SaveChangesAsync();

        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {


            var result = _dbSet.AsQueryable();

            if (includes != null)
                result = includes(result);

            return await Task.FromResult(predicate != null ? result.Where(predicate) : result);
        }

        public User GetUserById(Tkey id)
        {
            return _appContext.Users.FirstOrDefault(x => x.Id.Equals(id));
        }

        public NFT GetNFTById(Tkey id)
        {
            return _appContext.NFTs.FirstOrDefault(x => x.Id.Equals(id));
        }

        public User GetUserByWalletAddress(string address)
        {
            return _appContext.Users.FirstOrDefault(w => w.WalletAddress == address);
        }
    }
}
