using Piranha.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Piranha.AttributeBuilder
{
    public class PageTypeBuilder: ContentTypeBuilder<PageTypeBuilder, PageType>
    {
        public PageTypeBuilder(Api api): base(api)
        {

        }

        /// <summary>
        /// Builds the page types.
        /// </summary>
        public override void Build()
        {
            foreach (var type in types)
            {
                var pageType = GetContentType(type);

                if (pageType != null)
                    api.PageTypes.Save(pageType);
            }
        }

        #region Private methods
        /// <summary>
        /// Gets the possible page type for the given type.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The page type</returns>
        protected override PageType GetContentType(Type type)
        {
            var attr = type.GetTypeInfo().GetCustomAttribute<PageTypeAttribute>();

            if (attr != null)
            {
                if (string.IsNullOrWhiteSpace(attr.Id))
                    attr.Id = type.GetTypeInfo().Name;

                if (!string.IsNullOrEmpty(attr.Id) && !string.IsNullOrEmpty(attr.Title))
                {
                    var pageType = new PageType()
                    {
                        Id = attr.Id,
                        Title = attr.Title,
                        Route = attr.Route
                    };

                    foreach (var prop in type.GetTypeInfo().GetProperties(App.PropertyBindings))
                    {
                        var regionType = GetRegoinType(prop);

                        if (regionType != null)
                            pageType.Regions.Add(regionType);
                    }
                    return pageType;
                }
            }
            else
            {
                throw new ArgumentException($"Title is mandatory in PageTpeAttribute. No title provided for {type.GetTypeInfo().Name}");
            }
            return null;
        }
        #endregion
    }
}
