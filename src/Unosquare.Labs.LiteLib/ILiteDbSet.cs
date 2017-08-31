﻿namespace Unosquare.Labs.LiteLib
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides basic DDL and CRUD command definitions and a DI-supplied Context
    /// </summary>
    public interface ILiteDbSet
    {
        /// <summary>
        /// Gets or sets the type of the entity.
        /// </summary>
        Type EntityType { get; set; }

        /// <summary>
        /// Gets the name of the data-backing table.
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// Gets the table definition.
        /// </summary>
        string TableDefinition { get; }

        /// <summary>
        /// Gets the select command definition.
        /// </summary>
        string SelectDefinition { get; }

        /// <summary>
        /// Gets the insert command definition.
        /// </summary>
        string InsertDefinition { get; }

        /// <summary>
        /// Gets the update command definition.
        /// </summary>
        string UpdateDefinition { get; }

        /// <summary>
        /// Gets the delete command definition.
        /// </summary>
        string DeleteDefinition { get; }

        /// <summary>
        /// Gets or sets the parent set context.
        /// </summary>
        LiteDbContext Context { get; set; }

    }

    /// <summary>
    /// Provides typed access to querying the database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILiteDbSet<T> : ILiteDbSet
        where T : ILiteModel
    {

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Returns an int</returns>
        int Insert(T entity);

        /// <summary>
        /// Deletes the specified entity. RowId must be set.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Returns an int</returns>
        int Delete(T entity);

        /// <summary>
        /// Updates the specified entity in a non optimistic concurrency manner.
        /// RowId must be set.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Returns an int</returns>
        int Update(T entity);

        /// <summary>
        /// Selects a set of entities from the database.
        /// Example whereText = "X = @X" and whereParames = new { X = "hello" }
        /// </summary>
        /// <param name="whereText">The where text.</param>
        /// <param name="whereParams">The where parameters.</param>
        /// <returns>Returns an IEnumerable with generic type</returns>
        IEnumerable<T> Select(string whereText, object whereParams);

        /// <summary>
        /// Selects all entities from the database.
        /// </summary>
        /// <returns>Returns an IEnumerable with generic type</returns>
        IEnumerable<T> SelectAll();

        /// <summary>
        /// Selects a single entity from the databse given its row id.
        /// </summary>
        /// <param name="rowId">The row identifier.</param>
        /// <returns>Returns a generic type</returns>
        T Single(long rowId);

        /// <summary>
        /// Counts the total number of rows in the table
        /// </summary>
        /// <returns>Returns an int</returns>
        int Count();

        /// <summary>
        /// Provides and asynchronous counterpart to the Insert method
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Returns a Task of type int</returns>
        Task<int> InsertAsync(T entity);

        /// <summary>
        /// Provides and asynchronous counterpart to the Delete method
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Returns a Task of type int</returns>
        Task<int> DeleteAsync(T entity);

        /// <summary>
        /// Provides and asynchronous counterpart to the Update method
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Returns a Task of type int</returns>
        Task<int> UpdateAsync(T entity);

        /// <summary>
        /// Provides and asynchronous counterpart to the Select method
        /// </summary>
        /// <param name="whereText">The where text.</param>
        /// <param name="whereParams">The where parameters.</param>
        /// <returns>Returns a Task of type IEnumerable with a generic type</returns>
        Task<IEnumerable<T>> SelectAsync(string whereText, object whereParams);

        /// <summary>
        /// Selects all asynchronous.
        /// </summary>
        /// <returns>Returns a Task of type IEnumerable with a generic type</returns>
        Task<IEnumerable<T>> SelectAllAsync();

        /// <summary>
        /// Provides and asynchronous counterpart to the Single method
        /// </summary>
        /// <param name="rowId">The row identifier.</param>
        /// <returns>Returns a Task with a generyc type</returns>
        Task<T> SingleAsync(long rowId);

        /// <summary>
        /// Provides and asynchronous counterpart to the Count method
        /// </summary>
        /// <returns>Returns a Task with a generyc type</returns>
        Task<int> CountAsync();
    }
}
