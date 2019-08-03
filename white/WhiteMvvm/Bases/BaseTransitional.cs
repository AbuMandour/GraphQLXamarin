using System;
using System.Collections.Generic;
using System.Text;
using Realms;

namespace WhiteMvvm.Bases
{
    public class BaseTransitional
    {
        /// <summary>
        /// convert transitional object to realm model object
        /// </summary>
        /// <typeparam name="TRealmObject"></typeparam>
        /// <returns></returns>
        public virtual TRealmObject ToRealmModel<TRealmObject>() where TRealmObject : RealmObject, new()
        {
            return new TRealmObject();
        }
        /// <summary>
        /// convert transitional object to model object
        /// </summary>
        /// <typeparam name="TBaseModel"></typeparam>
        /// <returns></returns>
        public virtual TBaseModel ToModel<TBaseModel>() where TBaseModel : BaseModel, new()
        {
            return new TBaseModel();
        }
    }
}
