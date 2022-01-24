using System;
using System.Collections.Generic;

namespace API.PropertiesUS.DAL.Repo
{
    /// <summary>
    /// Interface class for base class of repositories for each entity in database
    /// </summary>
    /// <typeparam name="T">Object type reference to be inherited by the class</typeparam>
    public interface IBaseRepo<T> where T : class
    {
        /// <summary>
        /// Method to get all the records of an entity
        /// </summary>
        /// <returns>List of records obtained from the inherited data type</returns>
        List<T> GetAll();
        /// <summary>
        /// Method to get records of an entity depending on search filters
        /// </summary>
        /// <param name="where">Query with filters for the entity, with Linq library</param>
        /// <returns>List of records obtained from the inherited data type</returns>
        List<T> GetListByFilter(Func<T, bool> where);
        /// <summary>
        /// Method to get a record of an entity depending on search filters
        /// </summary>
        /// <param name="where">Query with filters for the entity, with Linq library</param>
        /// <returns>Object/registry obtained from the inherited data type</returns>
        T GetByFilter(Func<T, bool> where);
        /// <summary>
        /// Method to add or create a record to the entity
        /// </summary>
        /// <param name="data">New record data</param>
        /// <returns>Object of the inherited data type, with inserted data</returns>
        T Add(T data);
        /// <summary>
        /// Method to update an entity record
        /// </summary>
        /// <param name="data">Row data to update</param>
        /// <returns>Object of inherited data type, with updated data</returns>
        T Update(T data);
        /// <summary>
        /// Method to get the number of records in the entity
        /// </summary>
        /// <param name="where">Query with filters for the entity, with Linq library</param>
        /// <returns>Number of records obtained from the entity</returns>
        long Count(Func<T, bool> where);
    }
}
