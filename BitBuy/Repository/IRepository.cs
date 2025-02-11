using BitBuy.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BitBuy.Repository
{
    public interface IRepository<T, Tkey> where T : class where Tkey : struct
    {
        Task AddAsync(T obj);

        Task EditUser(T obj);

        Task SaveChangesAsync();

        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        User GetUserById(Tkey id);
        NFT GetNFTById(Tkey id);
        User GetUserByWalletAddress(string address);
    }
}
