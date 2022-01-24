using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.PropertiesUS.DAL.Repo
{
    /// <summary>
    /// Base class to use repositories of each entity in database
    /// </summary>
    /// <typeparam name="T">Object type reference to be inherited by the class</typeparam>
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        /// <summary>
        /// Object for database use as DBContext
        /// </summary>
        public readonly DbContextPropertiesUS _context = null;
        /// <summary>
        /// Object representing an inherited entity
        /// </summary>
        private readonly DbSet<T> _dbEntity;

        /// <summary>
        /// Constructor method of the class
        /// </summary>
        /// <param name="connectionString">String with database connection string (nullable)</param>
        public BaseRepo(string connectionString = null)
        {
            if (_context == null)
            {
                _context = new DbContextPropertiesUS(connectionString);
            }
            _dbEntity = _context.Set<T>();
        }

        /// <summary>
        /// Method to get all the records of an entity
        /// </summary>
        /// <returns>List of records obtained from the inherited data type</returns>
        public List<T> GetAll()
        {
            try
            {
                List<T> datos = new List<T>();
                datos = _dbEntity.ToList();
                return datos;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Method to get records of an entity depending on search filters
        /// </summary>
        /// <param name="where">Query with filters for the entity, with Linq library</param>
        /// <returns>List of records obtained from the inherited data type</returns>
        public List<T> GetListByFilter(Func<T, bool> where)
        {
            try
            {
                List<T> datos = new List<T>();
                datos = _dbEntity.Where(where).ToList();
                return datos;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Method to get a record of an entity depending on search filters
        /// </summary>
        /// <param name="where">Query with filters for the entity, with Linq library</param>
        /// <returns>Object/registry obtained from the inherited data type</returns>
        public T GetByFilter(Func<T, bool> where)
        {
            try
            {
                T dato = _dbEntity.Where(where).FirstOrDefault();
                return dato;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Method to add or create a record to the entity
        /// </summary>
        /// <param name="data">New record data</param>
        /// <returns>Object of the inherited data type, with inserted data</returns>
        public T Add(T data)
        {
            try
            {
                _dbEntity.Add(data);
                _context.SaveChanges();
                return data;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Method to update an entity record
        /// </summary>
        /// <param name="data">Row data to update</param>
        /// <returns>Object of inherited data type, with updated data</returns>
        public T Update(T data)
        {
            try
            {
                _dbEntity.Update(data);
                _context.SaveChanges();
                return data;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Method to get the number of records in the entity
        /// </summary>
        /// <param name="where">Query with filters for the entity, with Linq library</param>
        /// <returns>Number of records obtained from the entity</returns>
        public long Count(Func<T, bool> where) 
        {
            try
            {
                long count = _dbEntity.Count(where);
                return count;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
