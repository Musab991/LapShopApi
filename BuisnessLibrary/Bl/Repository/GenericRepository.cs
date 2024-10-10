

namespace BuisnessLibrary.Bl.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<TEntity> _dbSet;

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
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _dbSet.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException(nameof(entities));
            }
            _dbSet.AddRange(entities);

                _appDbContext.SaveChanges();
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException(nameof(entities));
            }

            await _dbSet.AddRangeAsync(entities);
            await _appDbContext.SaveChangesAsync();
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
        public void DeleteRange(Expression<Func<TEntity, bool>> filter)
        {
            var entities = Find(filter);
            _dbSet.RemoveRange(entities);
            _appDbContext.SaveChanges();
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> filter, string[]? includes = null)
          => BuildQuery(filter, null, null, null, null, includes).FirstOrDefault();

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter,
                                 Expression<Func<TEntity, object>>? orderBy = null,
                                 string orderByDirection = OrderBy.Ascending,
                                 int? skip = null,
                                 int? take = null,
                                 string[]? includes = null)
              => BuildQuery(filter, orderBy, orderByDirection, skip, take, includes).ToList();

        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter, string[]? includes = null)
             => await BuildQuery(filter, null, null, null, null, includes).SingleOrDefaultAsync();

        public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, string[]? includes = null)
                      => await BuildQuery(filter, null, null, null, null, includes).ToListAsync();
    
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, int? skip, int? take,
           Expression<Func<TEntity, object>> ?orderBy = null, string orderByDirection = OrderBy.Ascending, string[]? includes = null)
            =>await BuildQuery(filter,orderBy, orderByDirection, skip,take,includes).ToListAsync();

        public IQueryable<TEntity> GetAll(string[]? includes = null)
        => BuildQuery(null, null, null, null, null, includes);
        public async Task<IEnumerable<TEntity>> GetAllAsync(string[]? includes = null)
        => await BuildQuery(null, null, null, null, null, includes).ToListAsync();


        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _appDbContext.SaveChanges();
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        => _dbSet.Any(filter);
        public int Count(Expression<Func<TEntity, bool>> filter)
        => _dbSet.Count(filter);

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await BuildQuery(filter).CountAsync();
        }

  
        private IQueryable<TEntity> BuildQuery(Expression<Func<TEntity,bool>>?filter=null,
            Expression<Func<TEntity, object>>? orderBy = null,string? orderByDirection=OrderBy.Ascending,
            int ?skip=null,int? take = null, string[]? includes = null)
        {

            IQueryable<TEntity> query = _dbSet;

            // Apply includes if provided
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    // Validate to ensure the include string is not null or empty
                    if (!string.IsNullOrWhiteSpace(include))
                    {
                        query = query.Include(include);
                    }
                }
            }

            // Apply filtering if provided
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Apply ordering
            if (orderBy != null)
            {
                query = orderByDirection == OrderBy.Ascending
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);
            }

            // Apply pagination if provided
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
        
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
       

}
