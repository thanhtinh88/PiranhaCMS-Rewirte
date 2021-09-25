using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Models
{
    public class PageRegionList<T> : List<T>, IRegionList
    {
        public string TypeId { get; set; }
        public string RegionId { get; set; }

        public T Create(Api api)
        {
            return (T)DynamicPage.CreateRegion(api, TypeId, RegionId);
        }

        public T CreateAndAdd(Api api)
        {
            var item = Create(api);
            Add(item);
            return item;
        }

        public void Add(object item)
        {
            if (item.GetType() == typeof(T))
                base.Add((T)item);
            else
                throw new ArgumentException();
        }
    }
}
