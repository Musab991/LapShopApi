using DomainLibrary.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(string[]? includes = null);
        T FindOne(Expression<Func<T, bool>> Filter, string[]? includes = null);
        Task<T> FindOneAsync(Expression<Func<T, bool>> Filter, string[]? includes = null);
     
        public IEnumerable<T> Find(Expression<Func<T, bool>> Filter,
          Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending, 
          int? skip=null, int? take= null, string[]? includes = null);
        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> Filter, int? skip, int? take,
        Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending, string[]? includes = null);
        void Add(T entity);
        Task<T>AddAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(Expression<Func<T, bool>> Filter);
        bool Any(Expression<Func<T, bool>> Filter);
        int Count(Expression<Func<T, bool>> Filter);
        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> filter);
   
    }

}
