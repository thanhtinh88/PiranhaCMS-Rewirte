using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Piranha.Models
{
    // TODO: Understand how content factory works
    internal class ContentFactory: IDisposable
    {
        #region Members
        private readonly IEnumerable<Extend.ContentType> types;
        #endregion

        public ContentFactory(IEnumerable<Extend.ContentType> types)
        {
            this.types = types;
        }

        public T Create<T>(string typeId) where T: Content
        {
            var contentType = types.SingleOrDefault(t => t.Id == typeId);

            if (contentType != null)
            {
                var model = (T)Activator.CreateInstance<T>();
                model.TypeId = typeId;

                if (model is PageModel)
                {
                    var dynModel = (PageModel)(object)model;

                    foreach (var region in contentType.Regions)
                    {
                        object value = null;
                        if (region.Collection)
                        {
                            var reg = CreateRegion(region);
                            if(reg != null)
                            {
                                value = Activator.CreateInstance(typeof(PageRegionList<>).MakeGenericType(reg.GetType()));
                                ((IRegionList)value).TypeId = typeId;
                                ((IRegionList)value).RegionId = region.Id;
                            }
                        }
                        else
                        {
                            value = CreateRegion(region);
                        }

                        if (value != null)
                            ((IDictionary<string, object>)dynModel.Regions).Add(region.Id, value);
                    }
                }
                else
                {
                    var typeInfo = model.GetType().GetTypeInfo();

                    foreach (var region in contentType.Regions)
                    {
                        if (!region.Collection)
                        {
                            var prop = typeInfo.GetProperty(region.Id, App.PropertyBindings);
                            if (prop != null)
                                prop.SetValue(model, CreateRegion(prop.PropertyType, region));
                        }
                    }
                }

                return model;
            }

            return null;
        }

        public object CreateRegion(string typeId, string regionId)
        {
            var contentType = types.SingleOrDefault(t => t.Id == typeId);
            
            if (contentType != null)
            {
                var region = contentType.Regions.SingleOrDefault(r => r.Id == regionId);
                if (region != null)
                    return CreateRegion(region);
            }
            return null;
        }

        public TValue CreateRegion<TValue>(string typeId, string regionId)
        {
            var contentType = types.SingleOrDefault(t => t.Id == typeId);
            if (contentType != null)
            {
                var region = contentType.Regions.SingleOrDefault(r => r.Id == regionId);
                if (region != null)
                    return (TValue)CreateRegion(typeof(TValue), region);
            }
            return default(TValue);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #region Private methods
        private object CreateRegion(Extend.RegionType region)
        {
            if (region.Fields.Count == 1)
            {
                var type = App.Fields.GetByShorthand(region.Fields[0].Type);
                if (type == null)
                    type = App.Fields.GetByType(region.Fields[0].Type);

                if (type != null)
                    return Activator.CreateInstance(type.Type);
            }
            else
            {
                var reg = new ExpandoObject();

                foreach (var field in region.Fields)
                {
                    var type = App.Fields.GetByShorthand(field.Type);
                    if (type == null)
                        type = App.Fields.GetByType(field.Type);
                    if (type != null)
                        ((IDictionary<string, object>)reg).Add(field.Id, Activator.CreateInstance(type.Type));
                }

                return reg;
            }
            return null;
        }

        private object CreateRegion(Type regionType, Extend.RegionType region)
        {
            if (region.Fields.Count == 1)
            {
                var type = App.Fields.GetByShorthand(region.Fields[0].Type);
                if (type == null)
                    type = App.Fields.GetByType(region.Fields[0].Type);

                if (type != null && type.Type == regionType)
                    return Activator.CreateInstance(type.Type);
                return null;
            }
            else
            {
                var reg = Activator.CreateInstance(regionType);
                var typeInfo = reg.GetType().GetTypeInfo();

                foreach (var field in region.Fields)
                {
                    var type = App.Fields.GetByShorthand(field.Type);
                    if (type == null)
                        type = App.Fields.GetByType(field.Type);

                    if (type != null)
                    {
                        var prop = typeInfo.GetProperty(field.Id, App.PropertyBindings);
                        if (prop != null && type.Type == prop.PropertyType)
                            prop.SetValue(reg, Activator.CreateInstance(type.Type));
                    }                  

                }
                return reg;
            }
        }
        #endregion
    }
}
