using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace MyWeddingSystem.Models.Context
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected DbContext databaseContext;
        protected DbSet<TEntity> dbSet;

        public BaseRepository(DbContext context)
        {
            databaseContext = context;
            dbSet = context.Set<TEntity>();
        }

        public TEntity Insert(TEntity entity)
        {
            dbSet.Add(entity);
            SaveChanges();

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            databaseContext.Entry(entity).State = EntityState.Modified;
            SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            TEntity entity = dbSet.Find(id);
            dbSet.Remove(entity);
            SaveChanges();
        }

        public List<TEntity> GetAll()
        {
            return dbSet.AsQueryable().ToList();
        }

        public TEntity Get(int id)
        {
            return dbSet.Find(id);
        }

        private void SaveChanges()
        {
            databaseContext.SaveChanges();
        }
    }
}