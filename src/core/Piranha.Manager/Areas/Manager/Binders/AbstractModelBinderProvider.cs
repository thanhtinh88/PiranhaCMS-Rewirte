﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Areas.Manager.Binders
{
    /// <summary>
    /// Binder provider for handling abstract types.
    /// </summary>
    public class AbstractModelBinderProvider: IModelBinderProvider
    {
        /// <summary>
        /// Gets the correct binder for the context.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <returns>The model binder</returns>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context != null)
            {
                // we only care aobut regions & data
                if (context.Metadata.ModelType == typeof(Models.PageEditRegionBase) || context.Metadata.ModelType == typeof(Extend.IField))
                {
                    var binders = new Dictionary<string, AbstractBinderType>();

                    var metadata = context.MetadataProvider.GetMetadataForType(typeof(Models.PageEditRegion));
                    binders.Add(typeof(Models.PageEditRegion).FullName, new AbstractBinderType()
                    {
                        Type = typeof(Models.PageEditRegion),
                        Binder = context.CreateBinder(metadata)
                    });

                    metadata = context.MetadataProvider.GetMetadataForType(typeof(Models.PageEditRegionCollection));
                    binders.Add(typeof(Models.PageEditRegionCollection).FullName, new AbstractBinderType()
                    {
                        Type = typeof(Models.PageEditRegionCollection),
                        Binder = context.CreateBinder(metadata)
                    });

                    foreach (var fieldType in App.Fields)
                    {
                        metadata = context.MetadataProvider.GetMetadataForType(fieldType.Type);
                        binders.Add(fieldType.CLRType, new AbstractBinderType()
                        {
                            Type = fieldType.Type,
                            Binder = context.CreateBinder(metadata)
                        });
                    }
                    return new AbstractModelBinder(context.MetadataProvider, binders);
                }
                return null;
            }
            throw new ArgumentNullException(nameof(context));
        }
    }
}
