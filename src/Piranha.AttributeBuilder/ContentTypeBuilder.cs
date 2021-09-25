﻿using Piranha.Extend;
using Piranha.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Piranha.AttributeBuilder
{
    public abstract class ContentTypeBuilder<T, TType> 
        where T: ContentTypeBuilder<T, TType> 
        where TType : ContentType
    {
        #region Members
        protected readonly List<Type> types = new List<Type>();
        protected readonly Api api;
        #endregion

        public ContentTypeBuilder(Api api)
        {
            this.api = api;
        }

        /// <summary>
        /// Adds a new type to build page types from
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The builder</returns>
        public T AddType(Type type)
        {
            types.Add(type);

            return (T)this;
        }

        /// <summary>
        /// Builds the page types.
        /// </summary>
        public abstract void Build();

        #region Private methods
        /// <summary>
        /// Gets the possible content type for the given type.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The content type</returns>
        protected abstract TType GetContentType(Type type);

        /// <summary>
        /// Gets the possible region type for the given property.
        /// </summary>
        /// <param name="prop">The property info</param>
        /// <returns>The region type</returns>
        protected RegionType GetRegoinType(PropertyInfo prop)
        {
            var attr = prop.GetCustomAttribute<RegionAttribute>();

            if (attr != null)
            {
                var isCollection = typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(prop.PropertyType);
                var regionType = new RegionType()
                {
                    Id = prop.Name,
                    Title = attr.Title,
                    Collection = isCollection,
                    Max = attr.Max,
                    Min = attr.Min
                };

                Type type = null;

                if (!isCollection)
                {
                    type = prop.PropertyType;
                }
                else
                {
                    type = prop.PropertyType.GenericTypeArguments.First();
                }

                if (typeof(IField).GetTypeInfo().IsAssignableFrom(type))
                {
                    var appFieldType = App.Fields.GetByType(type);

                    if (appFieldType == null)
                    {
                        // This is a single field region, but the type is missing, discard the entire region
                        return null;
                    }

                    regionType.Fields.Add(new FieldType()
                    {
                        Id = "Default",
                        Type = appFieldType.TypeName
                    });
                }
                else
                {
                    foreach (var fieldProp in type.GetTypeInfo().GetProperties(App.PropertyBindings))
                    {
                        var fieldType = GetFieldType(fieldProp);

                        if (fieldType != null)
                            regionType.Fields.Add(fieldType);
                    }

                    // skip regions without fields
                    if (regionType.Fields.Count == 0)
                        return null;
                }
                return regionType;
            }
            return null;
        }

        /// <summary>
        /// Gets the possible field type for the given property.
        /// </summary>
        /// <param name="prop">The property</param>
        /// <returns>The field type</returns>
        protected FieldType GetFieldType(PropertyInfo prop)
        {
            var attr = prop.GetCustomAttribute<FieldAttribute>();

            if (attr != null)
            {
                var appFieldType = App.Fields.GetByType(prop.PropertyType);

                if (appFieldType != null)
                {
                    return new FieldType()
                    {
                        Id = prop.Name,
                        Title = attr.Title,
                        Type = appFieldType.TypeName
                    };
                }
            }
            return null;
        }
        #endregion
    }
}
