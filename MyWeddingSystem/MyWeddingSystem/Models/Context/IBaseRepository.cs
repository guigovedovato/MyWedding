using System.Collections.Generic;

namespace MyWeddingSystem.Models.Context
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(int id);

        List<TEntity> GetAll();

        TEntity Get(int id);
    }
}