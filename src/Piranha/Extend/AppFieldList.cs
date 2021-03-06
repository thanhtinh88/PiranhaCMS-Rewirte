using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Piranha.Extend
{
    public sealed class AppFieldList: AppDataList<IField, AppField>
    {
        /// <summary>
        /// Gets a single item by its shorthand name.
        /// </summary>
        /// <param name="shorthand">The shorthand name</param>
        /// <returns>The item, null if not found</returns>
        public AppField GetByShorthand(string shorthand)
        {
            return items.FirstOrDefault(i => i.Shorthand == shorthand);
        }

        /// <summary>
        /// Performs additional processing on the item before
        /// adding it to the collection.
        /// </summary>
        /// <typeparam name="TValue">The value type</typeparam>
        /// <param name="item">The item</param>
        /// <returns>The processed item</returns>
        protected override AppField OnRegister<TValue>(AppField item)
        {
            var attr = typeof(TValue).GetTypeInfo().GetCustomAttribute<Extend.FieldAttribute>();
            if (attr != null)
            {
                item.Name = attr.Name;
                item.Shorthand = attr.Shorthand;
            }

            return item;
        }
    }
}
