using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Realms;
using WhiteMvvm.Bases;


namespace WhiteMvvm.Utilities
{
    public class TransitionalList<TBaseTransition> : Collection<TBaseTransition> where TBaseTransition : BaseTransitional
    {
        public virtual ObservableRangeCollection<TRealmObject> ToRealmModel<TRealmObject>() where TRealmObject : RealmObject , new()
        {
            var newList = new ObservableRangeCollection<TRealmObject>();
            foreach (var transition in this)
            {                
                var realmObject = transition.ToRealmModel<TRealmObject>();
                newList.Add(realmObject);
            }
            return newList;
        }
        public virtual ObservableRangeCollection<TBaseModel> ToModel<TBaseModel>() where TBaseModel : BaseModel, new()
        {
            var newList = new ObservableRangeCollection<TBaseModel>();
            foreach (var transition in this)
            {
                var realmObject = transition.ToModel<TBaseModel>();
                newList.Add(realmObject);
            }
            return newList;
        }
    }
}
