using BuisnessLibrary.Bl.Generic;
using BuisnessLibrary.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Bl.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<TEntity> _dbSet;

        //While Creating an Instance of GenericRepository, we need to pass the UnitOfWork instance
        //public GenericRepository(IUnitOfWork<AppDbContext> unitOfWork)
        //  : this(unitOfWork.Context)
        //{
        //}

        public GenericRepository(AppDbContext contextService)
        {
            _appDbContext = contextService;
            _dbSet = _appDbContext.Set<TEntity>(); // Generic DbSet for any entity T
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbSet.Add(entity);
            _appDbContext.SaveChanges();
        }
        public void AddMany(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));

                }
            }
            _dbSet.AddRange(entities);
            _appDbContext.SaveChanges();
        }
        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbSet.Remove(entity);
            _appDbContext.SaveChanges();
        }
        public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Find(predicate);
            _dbSet.RemoveRange(entities);
            _appDbContext.SaveChanges();
        }
        public TEntity FindOne(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
        {
            return Get(findOptions).FirstOrDefault(predicate)!;
        }
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
        {
            return Get(findOptions).Where(predicate);
        }
        public IQueryable<TEntity> GetAll(FindOptions? findOptions = null)
        {
            return Get(findOptions);
        }
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _appDbContext.SaveChanges();
        }
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Count(predicate);
        }
        private DbSet<TEntity> Get(FindOptions? findOptions = null)
        {
            findOptions ??= new FindOptions();
            var entity = _appDbContext.Set<TEntity>();
            if (findOptions.IsAsNoTracking && findOptions.IsIgnoreAutoIncludes)
            {
                entity.IgnoreAutoIncludes().AsNoTracking();
            }
            else if (findOptions.IsIgnoreAutoIncludes)
            {
                entity.IgnoreAutoIncludes();
            }
            else if (findOptions.IsAsNoTracking)
            {
                entity.AsNoTracking();
            }
            return entity;
        }


    }

}
