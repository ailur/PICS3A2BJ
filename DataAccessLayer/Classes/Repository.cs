using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Context of the repository.
        /// Set on constructor.
        /// </summary>
        protected readonly DbContext Context;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="context">Context of the repository</param>
        public Repository(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Get a TEntity item
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns>TEntity item</returns>
        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Get all TEntity item
        /// </summary>
        /// <returns>TEntity items</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        /// <summary>
        /// Finds items that fulfill the predicate
        /// </summary>
        /// <param name="predicate">Query to find the item</param>
        /// <returns>Items found</returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// Adds an item to the context
        /// </summary>
        /// <param name="entity">Item to add</param>
        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Adds a collection of items to the context
        /// </summary>
        /// <param name="entities">Items to add</param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        /// <summary>
        /// Remove the item from the context
        /// </summary>
        /// <param name="entity">Item to remove</param>
        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Removes items from the context
        /// </summary>
        /// <param name="entities">Items to remove</param>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        /// <summary>
        /// Updates an item
        /// </summary>
        /// <param name="entity">Item to update</param>
        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Saves the context
        /// </summary>
        public void Save()
        {
            Context.SaveChanges();
        }
    }
}