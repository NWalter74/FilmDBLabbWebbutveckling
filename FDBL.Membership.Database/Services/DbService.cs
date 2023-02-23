namespace FDBL.Membership.Database.Services;

public class DbService : IDbService
{
    // To give the service access to the database
    private readonly FDBLContext _db;

    // To easily convert between DTO and Entity
    private readonly IMapper _mapper;

    //Inject the FDBLContext and AutoMapper (IMapper)
    public DbService(FDBLContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    /// The TEntity type is limited to only classes because an entity must be a class and the IEntity interface defining the Id property.
    public async Task<List<TDto>> GetAsync<TEntity, TDto>() where TEntity : class where TDto : class
    {
        var entities = await _db.Set<TEntity>().ToListAsync();
        return _mapper.Map<List<TDto>>(entities);
    }

    public async Task<List<TDto>> GetAsync<TEntity, TDto>(
                  Expression<Func<TEntity, bool>> expression)
                  where TEntity : class
                  where TDto : class
    {
        var entities = await _db.Set<TEntity>().Where(expression).ToListAsync();
        return _mapper.Map<List<TDto>>(entities);
    }


    private async Task<TEntity?> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity =>
    await _db.Set<TEntity>().SingleOrDefaultAsync(expression);
    public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity where TDto : class
    {
        var entity = await SingleAsync(expression);
        return _mapper.Map<TDto>(entity);
    }

    public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity =>
     await _db.Set<TEntity>().AnyAsync(expression);

    public async Task<bool> SaveChangesAsync() => await _db.SaveChangesAsync() >= 0;

    public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto) where TEntity : class where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _db.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public void Update<TEntity, TDto>(int id, TDto dto) where TEntity : class, IEntity where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);
        entity.Id = id;
        _db.Set<TEntity>().Update(entity);
    }

    public async Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity
    {
        try
        {
            var entity = await SingleAsync<TEntity>(e => e.Id.Equals(id));
            if (entity is null) return false;
            _db.Remove(entity);
        }
        catch { throw; }

        return true;
    }

    //  Includes related table data through eager loading by specifying the generic entity type when calling it.
    public void Include<TEntity>() where TEntity : class
    {
        //Use reflection to find the names of the navigation properties in the TEntity type.
        var propertyNames = _db.Model.FindEntityType(typeof(TEntity))?.GetNavigations().Select(e => e.Name);

        //Return if the result is null, i.e. the entity has no navigation properties.
        if (propertyNames is null)
        {
            return;
        }

        //Iterate over the property names and use EF to fetch the entity for IEntity and call the Include method with the name and load the related entity by calling the Load method.
        foreach (var name in propertyNames)
            _db.Set<TEntity>().Include(name).Load();
    }
    public void DeleteAsync<TEntity>(TEntity dto) where TEntity : class
    {
        _db.ChangeTracker.Clear();
        var entity = _mapper.Map<TEntity>(dto);
        _db.Set<TEntity>().Remove(entity);
    }

    public string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity

    => $"/{typeof(TEntity).Name.ToLower()}s/{entity.Id}";
}

