﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Piranha.Models;
using Piranha.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Piranha.EF.Repositories
{
    public class PageRepository : RepositoryBase<Data.Page, Models.PageModel>, IPageRepository
    {
        #region Members
        /// <summary>
        /// The current Api.
        /// </summary>
        private readonly Api api;
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="db">The current db context</param>
        internal PageRepository(Api api, Db db): base(db)
        {
            this.api = api;
        }

        /// <summary>
        /// Gets the site startpage.
        /// </summary>
        /// <returns>The page model</returns>
        public PageModel GetStartPage()
        {
            return GetStartPage<Models.PageModel>();
        }

        /// <summary>
        /// Gets the site startpage.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <returns>The page model</returns>
        public T GetStartPage<T>() where T : Models.PageModel<T>
        {
            var page = Query().SingleOrDefault(p => !p.ParentId.HasValue && p.SortOrder == 0);

            if (page != null)
                return Load<T>(page);
            return null;
        }

        /// <summary>
        /// Gets the page model with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <returns>The page model</returns>
        public override PageModel GetById(Guid id)
        {
            return GetById<PageModel>(id);
        }

        /// <summary>
        /// Gets the page model with the specified id.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="id">The unique id</param>
        public T GetById<T>(Guid id) where T : Models.PageModel<T>
        {
            var page = Query().SingleOrDefault(p => p.Id == id);

            if (page != null)
                return Load<T>(page);
            return null;
        }

        /// <summary>
        /// Gets the page model with the specified slug.
        /// </summary>
        /// <param name="slug">The unique slug</param>
        /// <returns>The page model</returns>
        public PageModel GetBySlug(string slug)
        {
            return GetBySlug<Models.PageModel>(slug);
        }

        /// <summary>
        /// Gets the page model with the specified slug.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="slug">The unique slug</param>
        /// <returns>The page model</returns>
        public T GetBySlug<T>(string slug) where T : Models.PageModel<T>
        {
            var page = Query().SingleOrDefault(p => p.Slug == slug);
            if (page != null)
                return Load<T>(page);
            return null;
        }

        public IList<PageModel> GetByParentId(Guid? parentId)
        {
            var result = new List<Models.PageModel>();
            var pages = Query()
                .Where(p => p.ParentId == parentId)
                .ToArray();

            foreach (var page in pages)
            {
                result.Add(Load<Models.PageModel>(page));
            }

            return result;
        }

        public void Save<T>(T model) where T : Models.PageModel<T>
        {
            var type = api.PageTypes.GetById(model.TypeId);
            if (type !=null)
            {
                var currentRegions = type.Regions.Select(r => r.Id).ToArray();

                // Check if we have the page in the database already
                var page = db.Pages
                    .Include(p => p.Fields)
                    .SingleOrDefault(p => p.Id == model.Id);

                // if not, create new page
                if (page == null)
                {
                    page = new Data.Page()
                    {
                        Id = model.Id == Guid.Empty ? model.Id : Guid.NewGuid(),
                        TypeId = model.TypeId
                    };
                    db.Pages.Add(page);
                    model.Id = page.Id;
                }

                // Map basic fields
                Module.Mapper.Map<Models.PageModelBase, Data.Page>(model, page);

                // Map regions
                foreach (var regionKey in currentRegions)
                {
                    // Check that the region exists in current model
                    if (HasRegion(model, regionKey))
                    {
                        var regionType = type.Regions.Single(r => r.Id == regionKey);

                        if (!regionType.Collection)
                        {
                            MapRegion(model, page, GetRegion(model, regionKey), regionType, regionKey);
                        }
                        else
                        {
                            // Remove all old fields for the current region
                            var oldFields = page.Fields.Where(f => f.RegionId == regionKey).ToArray();
                            if (oldFields.Length > 0)
                                db.PageFields.RemoveRange(oldFields);

                            var sortOrder = 0;
                            foreach (var region in GetEnumerable(model, regionKey))
                            {
                                MapRegion(model, page, region, regionType, regionKey, sortOrder++);
                            }
                        }
                    }
                }

                // Save everything for the model
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the base entity query.
        /// </summary>
        /// <returns>The query</returns>
        protected override IQueryable<Data.Page> Query()
        {
            return db.Pages
                .Include(p => p.Fields);
        }


        #region Private methods

        /// <summary>
        /// Loads the given data entity into a new model.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="page">The data entity</param>
        /// <returns>The page model</returns>
        private T Load<T>(Data.Page page) where T : Models.PageModel<T>
        {
            var type = api.PageTypes.GetById(page.TypeId);

            if (type != null)
            {
                // create an initialized model
                var model = (T)typeof(T).GetMethod("Create", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke(null, new object[] { page.TypeId });
                var currentRegions = type.Regions.Select(r => r.Id).ToArray();

                // Map basic fields
                Module.Mapper.Map<Data.Page, Models.PageModelBase>(page, model);

                // Map regions
                foreach (var regionKey in currentRegions)
                {
                    var region = type.Regions.Single(r => r.Id == regionKey);
                    var fields = page.Fields.Where(f => f.RegionId == regionKey).OrderBy(f => f.SortOrder).ToList();

                    if (!region.Collection)
                    {
                        foreach (var fieldDef in region.Fields)
                        {
                            var field = fields.SingleOrDefault(f => f.FieldId == fieldDef.Id && f.SortOrder == 0);

                            if (field != null)
                            {
                                if (region.Fields.Count ==1)
                                {
                                    SetSimpleValue(model, regionKey, field);
                                    break;
                                }
                                else
                                {
                                    SetComplexValue(model, regionKey, fieldDef.Id, field);
                                }
                            }
                        }
                    }
                    else
                    {
                        var sortOrder = 0;
                        do
                        {
                            if (region.Fields.Count == 1)
                            {
                                var field = fields.SingleOrDefault(f => f.FieldId == region.Fields[0].Id && f.SortOrder == sortOrder);
                                if (field != null)
                                    AddSimpleValue(model, regionKey, field);
                            }
                            else
                            {
                                AddComplexValue(model, regionKey, fields.Where(f => f.SortOrder == sortOrder).ToList());
                            }
                            sortOrder++;
                        } while (page.Fields.Count(f => f.SortOrder == sortOrder) > 0);
                    }
                }

                return model;
            }
            return null;
        }

        /// <summary>
        /// Checks if the given model has a region with the given id.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="model">The model</param>
        /// <param name="regionId">The region id</param>
        /// <returns>If the region exists</returns>
        private bool HasRegion<T>(T model, string regionId) where T : Models.PageModel<T>
        {
            if (model is Models.PageModel)
            {
                return ((IDictionary<string, object>)((Models.PageModel)(object)model).Regions).ContainsKey(regionId);
            }
            else
            {
                return model.GetType().GetProperty(regionId, App.PropertyBindings) != null;
            }
        }

        /// <summary>
        /// Gets a field value from a complex region.
        /// </summary>
        /// <param name="region">The region</param>
        /// <param name="fieldId">The field id</param>
        /// <returns>The value</returns>
        private object GetComplexValue(object region, string fieldId)
        {
            if (region is ExpandoObject)
            {
                return ((IDictionary<string, object>)region)[fieldId];
            }
            else
            {
                return region.GetType().GetProperty(fieldId, App.PropertyBindings).GetValue(region);
            }
        }

        /// <summary>
        /// Gets the enumerator for the given region collection.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="model">The model</param>
        /// <param name="regionId">The region id</param>
        /// <returns>The enumerator</returns>
        private IEnumerable GetEnumerable<T>(T model, string regionId) where T : PageModel<T>
        {
            object value = null;
            if (model is Models.PageModel)
            {
                value = ((IDictionary<string, object>)((Models.PageModel)(object)model).Regions)[regionId];
            }
            else
            {
                value = model.GetType().GetProperty(regionId, App.PropertyBindings).GetValue(model);
            }

            if (value is IEnumerable)
            {
                return (IEnumerable)value;
            }

            return null;
        }

        /// <summary>
        /// Gets the region with the given key.
        /// </summary>
        /// <typeparam name="TModelType">The model type</typeparam>
        /// <param name="model">The model</param>
        /// <param name="regionId">The region id</param>
        /// <returns>The region</returns>
        private object GetRegion<T>(T model, string regionId) where T : PageModel<T>
        {
            if (model is Models.PageModel)
            {
                return ((IDictionary<string, object>)((Models.PageModel)(object)model).Regions)[regionId];
            }
            else
            {
                return model.GetType().GetProperty(regionId, App.PropertyBindings).GetValue(model);
            }
        }

        /// <summary>
        /// Sets the value of a simple single field region.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="model">The model</param>
        /// <param name="regionId">The region id</param>
        /// <param name="field">The field</param>
        private void SetSimpleValue<T>(T model, string regionId, Data.PageField field) where T : PageModel<T>
        {
            var type = App.Fields.GetByType(field.CLRType);

            if (type != null)
            {
                if (model is Models.PageModel)
                {
                    ((IDictionary<string, object>)((Models.PageModel)(object)model).Regions)[regionId] = 
                        JsonConvert.DeserializeObject(field.Value, type.Type);
                }
                else
                {
                    model.GetType().GetProperty(regionId, App.PropertyBindings).SetValue(model,
                        JsonConvert.DeserializeObject(field.Value, type.Type));
                }
            }
        }

        /// <summary>
        /// Sets the value of a complex region.
        /// </summary>
        /// <typeparam name="T">The model</typeparam>
        /// <param name="model">The model</param>
        /// <param name="regionId">The region id</param>
        /// <param name="fieldId">The field id</param>
        /// <param name="field">The field</param>
        private void SetComplexValue<T>(T model, string regionId, string fieldId, Data.PageField field) where T : PageModel<T>
        {
            var type = App.Fields.GetByType(field.CLRType);

            if (type != null)
            {
                if (model is Models.PageModel)
                {
                    ((IDictionary<string, object>)((Models.PageModel)(object)model).Regions)[regionId] =
                        JsonConvert.DeserializeObject(field.Value, type.Type);
                }
                else
                {
                    var obj = model.GetType().GetProperty(regionId, App.PropertyBindings).GetValue(model);
                    if (obj != null)
                        obj.GetType().GetProperty(fieldId, App.PropertyBindings).SetValue(obj,
                            JsonConvert.DeserializeObject(field.Value, type.Type));
                }
            }
        }

        /// <summary>
        /// Adds a simple single field value to a collection region.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="model">The model</param>
        /// <param name="regionId">The region id</param>
        /// <param name="field">The field</param>
        private void AddSimpleValue<T>(T model, string regionId, Data.PageField field) where T : PageModel<T>
        {
            var type = App.Fields.GetByType(field.CLRType);

            if (type != null)
            {
                if (model is Models.PageModel)
                {
                    ((IList)((IDictionary<string, object>)((Models.PageModel)(object)model).Regions)[regionId]).Add(
                        JsonConvert.DeserializeObject(field.Value, type.Type));
                }
                else
                {
                    ((IList)model.GetType().GetProperty(regionId, App.PropertyBindings).GetValue(model)).Add(
                        JsonConvert.DeserializeObject(field.Value, type.Type));
                }
            }
        }

        /// <summary>
        /// Adds a complex region to a collection region.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="model">The model</param>
        /// <param name="regionId">The region id</param>
        /// <param name="fields">The field</param>
        private void AddComplexValue<T>(T model, string regionId, List<Data.PageField> pageFields) where T : PageModel<T>
        {
            if (model is Models.PageModel)
            {
                var list = (IList)((IDictionary<string, object>)((Models.PageModel)(object)model).Regions)[regionId];
                var obj = Models.PageModel.CreateRegion(model.TypeId, regionId);

                foreach (var field in pageFields)
                {
                    var type = App.Fields.GetByType(field.CLRType);
                    if (type != null)
                    {
                        if (((IDictionary<string, object>)obj).ContainsKey(field.FieldId))
                        {
                            ((IDictionary<string, object>)obj)[field.FieldId] =
                                JsonConvert.DeserializeObject(field.Value, type.Type);
                        }
                    }
                }
                list.Add(obj);
            }
            else
            {
                var list = (IList)model.GetType().GetProperty(regionId, App.PropertyBindings).GetValue(model);
                var obj = Activator.CreateInstance(list.GetType().GenericTypeArguments.First());

                foreach (var field in pageFields)
                {
                    var prop = obj.GetType().GetProperty(field.FieldId, App.PropertyBindings);
                    if (prop != null)
                    {
                        prop.SetValue(obj, JsonConvert.DeserializeObject(field.Value, prop.PropertyType));
                    }
                }
                list.Add(obj);
            }
        }

        /// <summary>
        /// Maps a region to the given data entity.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="model">The model</param>
        /// <param name="page">The data entity</param>
        /// <param name="region">The region to map</param>
        /// <param name="regionType">The region type</param>
        /// <param name="regionId">The region id</param>
        /// <param name="sortOrder">The optional sort order</param>
        private void MapRegion<T>(T model, Data.Page page, object region, Extend.RegionType regionType, string regionId, int sortOrder = 0) where T : PageModel<T>
        {
            // Now map all of ther fields
            for (int n = 0; n < regionType.Fields.Count; n++)
            {
                var fieldDef = regionType.Fields[n];
                var fieldType = App.Fields.GetByShorthand(fieldDef.Type);
                if (fieldType == null)
                    fieldType = App.Fields.GetByType(fieldDef.Type);

                if (fieldType != null)
                {
                    object fieldValue = null;
                    if (regionType.Fields.Count == 1)
                    {
                        // Get the field value for simple region
                        fieldValue = region; //GetSimpleValue(model, regionId);
                    }
                    else
                    {
                        // Get the field value for complex region
                        fieldValue = GetComplexValue(region, fieldDef.Id); // GetComplexValue(model, regionId, fieldDef.Id);
                    }

                    // Check that the returned value matches the type specified for the page type, 
                    // otherwise deserialization won't work when the model is retrieved from the database
                    if (fieldValue.GetType() != fieldType.Type)
                        throw new ArgumentException("Given page field value does not match the configured page type");

                    // Check if we have the current field in the database already
                    var field = page.Fields
                        .SingleOrDefault(f => f.RegionId == regionId && f.FieldId == fieldDef.Id && f.SortOrder == sortOrder);

                    // If not, create a new fields
                    if (field == null)
                    {
                        field = new Data.PageField()
                        {
                            PageId = page.Id,
                            RegionId = regionId,
                            FieldId = fieldDef.Id
                        };
                        db.PageFields.Add(field);
                    }

                    // Update field info & value
                    field.CLRType = fieldType.CLRType;
                    field.SortOrder = sortOrder;
                    field.Value = JsonConvert.SerializeObject(fieldValue);
                }
            }
        }


        #endregion
    }
}
