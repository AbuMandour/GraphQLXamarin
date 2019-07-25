using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvm.Bases;
using WhiteMvvm.Exceptions;
using WhiteMvvm.Services.DeviceUtilities;

namespace WhiteMvvm.Services.Cache.SqliteCache
{
    public class SqliteService : ISqliteService
    {
        private readonly IFileSystem _fileSystem;
        private readonly SQLiteConnection _sqLiteConnection;
        private object locker = new object();

        public SqliteService(IFileSystem fileSystem, SQLiteConnection sqLiteConnection)
        {
            _fileSystem = fileSystem;
            _sqLiteConnection = sqLiteConnection;
            _sqLiteConnection = CreateConnection();
        }
        public SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(_fileSystem.CacheDirectory);
        }
        /// <summary>
        /// method to create tables after check if it exists
        /// </summary>
        /// <returns></returns>
        public bool CreateDatabaseTables<T>(IList<T> tables) where T : BaseModel 
        {
            try
            {
                lock (locker)
                {
                    foreach (var table in tables)
                    {
                        if (!TableExists(table))
                        {
                            _sqLiteConnection.CreateTable(table.GetType());
                        }
                    }
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to create database" , exception);
            }
        }
        /// <summary>
        /// Checks the database to see if the table exists
        /// </summary>
        public bool TableExists<T>()
        {
            try
            {
                lock (locker)
                {
                    var cmd = _sqLiteConnection.CreateCommand("SELECT name FROM sqlite_master WHERE type='table' AND name=?", typeof(T).Name);
                    return cmd.ExecuteScalar<string>() != null;
                }
            }
            catch (Exception exception)
            {
                throw new SqliteException($"Unable to find {typeof(T).Name} table", exception);
            }
        }
        /// <summary>
        /// Checks the database to see if the table exists
        /// </summary>
        public bool TableExists<T>(T table)
        {
            try
            {
                lock (locker)
                {
                    var cmd = _sqLiteConnection.CreateCommand("SELECT name FROM sqlite_master WHERE type='table' AND name=?", table.GetType().Name);
                    return cmd.ExecuteScalar<string>() != null;
                }
            }
            catch (Exception exception)
            {
                throw new SqliteException($"Unable to find {table.GetType().Name} table", exception);
            }
        }

        public void CreateTable<T>() where T : BaseModel, new()
        {
            lock (locker)
            {
                try
                {
                    _sqLiteConnection.CreateTable<T>();
                }

                catch (Exception exception)
                {
                    throw new SqliteException($"Unable to create {typeof(T).Name} table", exception);
                }
            }
        }
        /// <summary>
        /// generic method return all rows in the table 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>list of generic object</returns>
        public List<T> Get<T>() where T : BaseModel, new()
        {
            lock (locker)
            {
                try
                {
                    if (!TableExists<T>())
                        CreateTable<T>();
                    return _sqLiteConnection.Table<T>().ToList();
                }
                catch (Exception exception)
                {
                    throw new SqliteException("Unable to get list", exception);
                }
            }
        }
        /// <summary>
        ///  generic method return some rows of table according to query
        /// <para>func to query in table with lamdba expression</para>       
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns>list of generic object </returns>
        public List<TResult> Get<TResult>(Expression<Func<TResult, bool>> query) where TResult : BaseModel, new()
        {
            try
            {
                lock (locker)
                {
                    if (!TableExists<TResult>())
                        CreateTable<TResult>();
                    return _sqLiteConnection.Table<TResult>().Where(query).ToList();
                }
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to get list", exception);
            }
        }
        /// <summary>
        /// generic method return row of table according to query
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns>item of generic object</returns>
        public TResult GetOne<TResult>(Expression<Func<TResult, bool>> query) where TResult : BaseModel, new()
        {
            try
            {
                lock (locker)
                {
                    if (!TableExists<TResult>())
                        CreateTable<TResult>();
                    return _sqLiteConnection.Table<TResult>().Where(query).AsQueryable().FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to get record", exception);
            }
        }
        /// <summary>
        /// static generic method to save an item in its table 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns>boolean</returns>
        public bool SaveAndUpdate<T>(T item) where T : BaseModel, new()
        {
            try
            {
                lock (locker)
                {
                    if (!TableExists<T>())
                    {
                        CreateTable<T>();
                        var id = _sqLiteConnection.Insert(item);
                    }
                    else
                    {
                        int result;
                        var serverItem = GetOne<T>(x => x.Id == item.Id);
                        if (serverItem == null)
                        {
                            result = _sqLiteConnection.Insert(item);
                        }
                        else
                        {
                            result = _sqLiteConnection.Update(serverItem);
                        }
                        return result > 0;
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to save or update item", exception);
            }
        }
        /// <summary>
        /// static generic method to save an item in its table 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns>boolean</returns>
        public bool Save<T>(T item) where T : BaseModel, new()
        {
            try
            {
                lock (locker)
                {
                    if (!TableExists<T>())
                    {
                        CreateTable<T>();
                        var id = _sqLiteConnection.Insert(item);
                    }
                    else
                    {
                        var result = _sqLiteConnection.Insert(item);
                        return result > 0;
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to save  item", exception);
            }
        }
        /// <summary>
        /// delete all items in table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int DeleteAll<T>()
        {
            int result;
            try
            {
                lock (locker)
                {
                    result = _sqLiteConnection.DeleteAll<T>();
                }
                return result;
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to delete items", exception);
            }
        }

        public int DeleteOne<T>(int id)
        {
            int result;
            try
            {
                lock (locker)
                {
                    result = _sqLiteConnection.Delete<T>(id);
                }
                return result;
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to delete item", exception);
            }
        }
    }
}
