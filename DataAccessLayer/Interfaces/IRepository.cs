using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get a TEntity item
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns>TEntity item</returns>
        TEntity Get(int id);
        /// <summary>
        /// Get all TEntity item
        /// </summary>
        /// <returns>TEntity items</returns>
        IEnumerable<TEntity> GetAll();
        /// <summary>
        /// Finds items that fulfill the predicate
        /// </summary>
        /// <param name="predicate">Query to find the item</param>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Adds an item to the context
        /// </summary>
        /// <param name="entity">Item to add</param>
        void Add(TEntity entity);
        /// <summary>
        /// Adds a collection of items to the context
        /// </summary>
        /// <param name="entities">Items to add</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Remove the item from the context
        /// </summary>
        /// <param name="entity">Item to remove</param>
        void Remove(TEntity entity);
        /// <summary>
        /// Removes items from the context
        /// </summary>
        /// <param name="entities">Items to remove</param>
        void RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates an item
        /// </summary>
        /// <param name="entity">Item to update</param>
        void Update(TEntity entity);
        /// <summary>
        /// Saves the context
        /// </summary>
        void Save();
    }

}