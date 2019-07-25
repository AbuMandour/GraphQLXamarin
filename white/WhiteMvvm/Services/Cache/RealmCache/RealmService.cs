using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Realms;

namespace WhiteMvvm.Services.Cache.RealmCache
{
    public class RealmService : IRealmService
    {
        private readonly Realm _realm = Realm.GetInstance();
        /// <inheritdoc />
        public T SaveItem<T>(T item) where T : RealmObject
        {
            if (item == null)
                return null;
            var newItem = new object();
            using (var trans = _realm.BeginWrite())
            {
                newItem = _realm.Add(item, true);
                trans.Commit();
            }
            return (T)newItem;
        }
        /// <inheritdoc />
        public bool SaveList<TRealmObject>(List<TRealmObject> items) where TRealmObject : RealmObject
        {
            using (var trans = _realm.BeginWrite())
            {
                foreach (var item in items)
                {
                    var newItem = _realm.Add(item, true);
                }
                trans.Commit();
            }
            return true;
        }
        /// <inheritdoc />
        public T GetItem<T>() where T : RealmObject
        {
            var item = _realm.All<T>().FirstOrDefault();
            return item;
        }
        /// <inheritdoc />
        public T GetItem<T>(Expression<Func<T, bool>> query) where T : RealmObject
        {
            var item = _realm.All<T>().FirstOrDefault(query);
            return item;
        }
        /// <inheritdoc />
        public IList<T> GetList<T>() where T : RealmObject
        {
            var items = _realm.All<T>();
            return items.ToList();
        }
        /// <inheritdoc />
        public IList<T> GetList<T>(Expression<Func<T, bool>> query) where T : RealmObject
        {
            var items = _realm.All<T>().Where(query);
            return items.ToList();
        }
        /// <inheritdoc />
        public bool DeleteItem<T>(T deletedObject) where T : RealmObject
        {
            _realm.Write((() =>
            {
                _realm.Remove(deletedObject);
            }));
            return true;
        }
        /// <inheritdoc />
        public bool DeleteList<T>() where T : RealmObject
        {
            _realm.Write((() =>
            {
                _realm.RemoveAll<T>();
            }));
            return true;
        }
        /// <inheritdoc />
        public void Update(Action action)
        {
            _realm.Write(action);
        }
    }
}
