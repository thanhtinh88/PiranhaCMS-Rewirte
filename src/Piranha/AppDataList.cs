﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha
{
    public abstract class AppDataList<T, TItem> : IEnumerable<TItem> where TItem : AppDataItem
    {
        #region Members
        /// <summary>
        /// The items collection.
        /// </summary>
        protected readonly List<TItem> items = new List<TItem>();
        #endregion

        /// <summary>
        /// Registers a new item.
        /// </summary>
        /// <typeparam name="TValue">The value type</typeparam>
        public virtual void Register<TValue>() where TValue: T
        {
            var type = typeof(TValue);

            // Make sure we don't register the same type multiple times.
            if (items.Where(i => i.Type == type).Count() == 0)
            {
                var item = Activator.CreateInstance<TItem>();

                item.Type = type;
                item.TypeName = type.FullName;

                items.Add(OnRegister<TValue>(item));
            }
        }

        /// <summary>
        /// Unregisters a previously registered item.
        /// </summary>
        /// <typeparam name="TValue">The value type</typeparam>
        public virtual void UnRegister<TValue>() where TValue: T
        {
            var item = items.SingleOrDefault(i => i.Type == typeof(TValue));
            if (item != null)
                items.Remove(item);
        }

        /// <summary>
        /// Gets a single item by its type.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The item, null if not found</returns>
        public virtual TItem GetByType(Type type)
        {
            return items.SingleOrDefault(i => i.Type == type);
        }

        /// <summary>
        /// Gets a single item by its full type name.
        /// </summary>
        /// <param name="typeName">The type name</param>
        /// <returns>The item, null if not found</returns>
        public virtual TItem GetByType(string typeName)
        {
            return items.SingleOrDefault(i => i.TypeName == typeName);
        }

        /// <summary>
        /// Gets the generic enumerator for the items.
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<TItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator for the items.
        /// </summary>
        /// <returns>The enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        /// <summary>
        /// Performs additional processing on the item before
        /// adding it to the collection.
        /// </summary>
        /// <typeparam name="TValue">The value type</typeparam>
        /// <param name="item">The item</param>
        /// <returns>The processed item</returns>
        protected virtual TItem OnRegister<TValue>(TItem item) where TValue: T
        {
            return item;
        }
    }
}
