using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Areas.Manager.Binders
{
    public sealed class AbstractModelBinder: IModelBinder
    {
        #region Members
        /// <summary>
        /// The meta data provider from the current binding context.
        /// </summary>
        private readonly IModelMetadataProvider provider;

        /// <summary>
        /// The available binders.
        /// </summary>
        private readonly Dictionary<string, AbstractBinderType> binders;
        #endregion

        public AbstractModelBinder(IModelMetadataProvider provider, Dictionary<string, AbstractBinderType> binders)
        {
            this.provider = provider;
            this.binders = binders;
        }

        /// <summary>
        /// Binds the current model from the context.
        /// </summary>
        /// <param name="bc">The binding context</param>
        /// <returns>An asynchronous task</returns>
        public async Task BindModelAsync(ModelBindingContext bc)
        {
            var result = ModelBindingResult.Failed();
            var typeName = "";

            // Get the requested abstract type
            if (bc.ModelType == typeof(Models.PageEditRegionBase))
                typeName = bc.ValueProvider.GetValue(bc.ModelName + ".CLRType").FirstValue;
            else if (bc.ModelType == typeof(Extend.IField))
                typeName = bc.ValueProvider.GetValue(bc.ModelName.Replace(".Value", "") + ".CLRType").FirstValue;

            if (!string.IsNullOrEmpty(typeName))
            {
                try
                {
                    if (binders.ContainsKey(typeName))
                    {
                        var item = binders[typeName];
                        var metadata = provider.GetMetadataForType(item.Type);

                        // Let the default binders take care of it once that the real type has been discovered
                        ModelBindingResult scoped;
                        using (bc.EnterNestedScope(metadata, bc.FieldName, bc.ModelName, model:null))
                        {
                            await item.Binder.BindModelAsync(bc);
                            scoped = bc.Result;
                        }
                        result = scoped;
                    }
                }
                catch { }
            }
            bc.Result = result;
        }
    }
}
