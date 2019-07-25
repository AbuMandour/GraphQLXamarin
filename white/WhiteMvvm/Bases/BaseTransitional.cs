using System;
using System.Collections.Generic;
using System.Text;
using Realms;

namespace WhiteMvvm.Bases
{
    public class BaseTransitional
    {
        /// <summary>
        /// convert transitional object to model object
        /// </summary>
        /// <typeparam name="TRealmObject"></typeparam>
        /// <returns></returns>
        public virtual TRealmObject ToModel<TRealmObject>() where TRealmObject : RealmObject, new()
        {
            return new TRealmObject();
        }
    }
}
