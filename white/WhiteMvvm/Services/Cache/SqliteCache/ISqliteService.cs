using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WhiteMvvm.Bases;

namespace WhiteMvvm.Services.Cache.SqliteCache
{
    public interface ISqliteService
    {
        bool CreateDatabaseTables<T>(IList<T> tables) where T : BaseModel;
        /// <summary>
        /// static generic method return all rows in the table 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>list of generic object</returns>
        List<T> Get<T>() where T : BaseModel, new();
        /// <summary>
        /// static generic method return some rows of table according to query
        /// <para>func to query in table with lamdba expression</para>       
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns>list of generic object </returns>
        List<TResult> Get<TResult>(Expression<Func<TResult, bool>> query) where TResult : BaseModel, new();
        /// <summary>
        /// static generic method return row of table according to query
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns>item of generic object</returns>
        TResult GetOne<TResult>(Expression<Func<TResult, bool>> query) where TResult : BaseModel, new();
        /// <summary>
        /// static generic method to save an item in its table 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns>int 0 if false , 1 if true </returns>
        bool SaveAndUpdate<T>(T item) where T : BaseModel, new();
        /// <summary>
        /// static generic method delete all table data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>int 0 if false , 1 if true</returns>
        int DeleteAll<T>();
        /// <summary>
        /// static generic method delete row from table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns>int 0 if false , 1 if true</returns>
        int DeleteOne<T>(int id);
        /// <summary>
        /// static generic method to save an item in its table 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns>int 0 if false , 1 if true </returns>
        bool Save<T>(T item) where T : BaseModel, new();

    }
}
