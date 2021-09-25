using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend
{
    public sealed class AppModuleList: AppDataList<IModule, AppModule>
    {
        /// <summary>
        /// Performs additional processing on the item before
        /// adding it to the collection.
        /// </summary>
        /// <typeparam name="TValue">The value type</typeparam>
        /// <param name="item">The item</param>
        /// <returns>The processed item</returns>
        protected override AppModule OnRegister<TValue>(AppModule item)
        {
            item.Instance = Activator.CreateInstance<TValue>();
            return item;
        }
    }
}
