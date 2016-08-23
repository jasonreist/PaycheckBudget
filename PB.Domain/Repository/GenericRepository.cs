namespace PB.Data.Domain.Repository
{
    using PB.Domain;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    //using System.Data.Linq;

    public class GenericRepository<TEntity> where TEntity : class
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository()
        {
            this.context = new PBEntities();
            this.context.Configuration.ProxyCreationEnabled = false;
            this.context.Configuration.LazyLoadingEnabled = false;
            this.dbSet = context.Set<TEntity>();
        }

        public GenericRepository(DbContext ctx)
        {
            this.context = ctx;
            this.dbSet = context.Set<TEntity>();

            this.context.Configuration.ProxyCreationEnabled = false;
            this.context.Configuration.LazyLoadingEnabled = false;
        }

        public virtual IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = dbSet;
            return query.AsParallel().ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            dbSet.Add(entity);

            return entity;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual DbContext GetContext()
        {
            return this.context;
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return dbSet.SqlQuery(query, parameters).ToList();
        }
    }
}

