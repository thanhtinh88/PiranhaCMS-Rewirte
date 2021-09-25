﻿using Dapper;
using Newtonsoft.Json;
using Piranha.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Piranha.Repositories
{
    public class PageRepository : IPageRepository
    {
        #region Members
        private readonly Api api;
        private readonly IDbConnection db;
        private readonly ICache cache;
        private readonly string table = "Piranha_Pages";
        private readonly string sort = "ParentId, SortOrder";
        #endregion

        public PageRepository(Api api, IDbConnection db, ICache cache)
        {
            this.api = api;
            this.db = db;
            this.cache = cache;
        }

        /// <summary>
        /// Gets all of the available pages for the current site.
        /// </summary>
        /// <param name="siteId">The optional site id</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The pages</returns>
        public IEnumerable<Models.DynamicPage> GetAll(Guid? siteId = null, IDbTransaction transaction = null)
        {
            if (!siteId.HasValue)
            {
                var site = api.Sites.GetDefault(transaction: transaction);

                if (site != null)
                    siteId = site.Id;
            }

            var pages = db.Query<Guid>(
                $"SELECT [Id] FROM [{table}] WHERE [SiteId]=@SiteId ORDER BY [ParentId], [SortOrder]",
                new { SiteId = siteId },
                transaction: transaction).ToArray();

            var models = new List<Models.DynamicPage>();
            foreach (var page in pages)
            {
                var model = GetById(page, transaction);
                if (model != null)
                    models.Add(model);
            }
            return models;
        }

        /// <summary>
        /// Gets the site startpage.
        /// </summary>
        /// <param name="siteId">The optional site id</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The page model</returns>
        public Models.DynamicPage GetStartPage(Guid? siteId = null, IDbTransaction transaction = null)
        {
            return GetStartPage<Models.DynamicPage>(siteId, transaction);
        }

        /// <summary>
        /// Gets the site startpage.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="siteId">The optional site id</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The page model</returns>
        public T GetStartPage<T>(Guid? siteId = null, IDbTransaction transaction = null) where T : Models.Page<T>
        {
            if (!siteId.HasValue)
            {
                var site = api.Sites.GetDefault(transaction: transaction);

                if (site != null)
                    siteId = site.Id;
            }

            var page = db.QueryFirstOrDefault<Page>(
                $"SELECT * FROM [{table}] WHERE [SiteId]=@SiteId AND [ParentId] IS NULL AND [SortOrder]=0",
                new { SiteId = siteId }, transaction: transaction);
            if (page != null)
            {
                page.Fields = db.Query<PageField>(
                    $"SELECT * FROM [Piranha_PageFields] WHERE [PageId]=@Id",
                    new { Id = page.Id },
                    transaction: transaction).ToList();

                return Load<T>(page);
            }
            return null;
        }

        /// <summary>
        /// Gets the page model with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The page model</returns>
        public Models.DynamicPage GetById(Guid id, IDbTransaction transaction = null)
        {
            return GetById<Models.DynamicPage>(id, transaction);
        }

        /// <summary>
        /// Gets the page model with the specified id.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="id">The unique id</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The page model</returns>
        public T GetById<T>(Guid id, IDbTransaction transaction = null) where T : Models.Page<T>
        {
            var multiple = db.QueryMultiple(
                $"SELECT * FROM [{table}] WHERE [Id]=@Id " +
                $"SELECT * FROM [Piranha_PageFields] WHERE [PageId]=@Id",
                new { Id = id },
                transaction: transaction);

            var page = multiple.Read<Page>().First();
            if (page != null)
                page.Fields = multiple.Read<PageField>().ToList();
            multiple.Dispose();

            if (page != null)
                return Load<T>(page);
            return null;
        }

        /// <summary>
        /// Gets the page model with the specified slug.
        /// </summary>
        /// <param name="slug">The unique slug</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The page model</returns>
        public Models.DynamicPage GetBySlug(string slug, IDbTransaction transaction = null)
        {
            return GetBySlug<Models.DynamicPage>(slug, transaction);
        }

        /// <summary>
        /// Gets the page model with the specified slug.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="slug">The unique slug</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The page model</returns>
        public T GetBySlug<T>(string slug, IDbTransaction transaction = null) where T : Models.Page<T>
        {
            var page = db.QueryFirstOrDefault<Page>(
                $"SELECT * FROM [{table}] WHERE [Slug]=@Slug",
                new { Slug = slug },
                transaction: transaction);

            if (page != null)
            {
                page.Fields = db.Query<PageField>(
                    $"SELECT * FROM [Piranha_PageFields] WHERE [PageId]=@Id",
                    new { Id = page.Id },
                    transaction: transaction).ToList();

                return Load<T>(page);
            }
            return null;            
        }

        /// <summary>
        /// Saves the given page model
        /// </summary>
        /// <param name="model">The page model</param>
        /// <param name="transaction">The optional transaction</param>
        public void Save<T>(T model, IDbTransaction transaction = null) where T : Models.Page<T>
        {
            var tx = transaction != null ? transaction : api.BeginTransaction();

            try
            {
                var type = api.PageTypes.GetById(model.TypeId, transaction: tx);
                var isNew = false;

                if (type != null)
                {
                    var currentRegions = type.Regions.Select(r => r.Id).ToArray();

                    // Check if we have the page in the database already
                    var multiple = db.QueryMultiple(
                        $"SELECT * FROM [{table}] WHERE [Id]=@Id " +
                        $"SELECT * FROM [Piranha_PageFields] WHERE [PageId]=@Id",
                        new { Id = model.Id },
                        transaction: tx);

                    var page = multiple.Read<Page>().FirstOrDefault();
                    if (page != null)
                        page.Fields = multiple.Read<PageField>().ToList();
                    multiple.Dispose();

                    // If not, create a new page
                    if (page == null)
                    {
                        page = new Page()
                        {
                            Id = model.Id != Guid.Empty ? model.Id : Guid.NewGuid(),
                            PageTypeId = model.TypeId,
                            Created = DateTime.Now,
                            LastModified = DateTime.Now
                        };
                        model.Id = page.Id;
                        isNew = true;

                        // Make room for the new page
                        MovePages(model.ParentId, model.SortOrder, true, tx);
                    }
                    else
                    {
                        page.LastModified = DateTime.Now;

                        // Check if the page has been moved
                        if (page.ParentId != model.ParentId || page.SortOrder != model.SortOrder)
                        {
                            // Remove the old position for the page
                            MovePages(page.ParentId, page.SortOrder + 1, false, tx);

                            // Add room for the new position of the page
                            MovePages(model.ParentId, model.SortOrder, true, tx);
                        }
                    }

                    // Ensure that we have a slug
                    if (string.IsNullOrWhiteSpace(model.Slug))
                        model.Slug = Utils.GenerateSlug(model.NavigationTitle != null ? model.NavigationTitle : model.Title);
                    else
                        model.Slug = Utils.GenerateSlug(model.Slug);

                    // Map basic fields
                    App.Mapper.Map<Models.PageBase, Page>(model, page);

                    // Save the page
                    if (isNew)
                    {
                        db.Execute(
                            $"INSERT INTO [{table}] ([Id], [PageTypeId], [SiteId], [ParentId], [SortOrder], [Title], [NavigationTitle], [Slug], [IsHidden], [MetaKeywords], [MetaDescription], [Route], [Published], [Created], [LastModified]) VALUES(@Id, @PageTypeId, @SiteId, @ParentId, @SortOrder, @Title, @NavigationTitle, @Slug, @IsHidden, @MetaKeywords, @MetaDescription, @Route, @Published, @Created, @LastModified)",
                            page, transaction: tx);
                    }
                    else
                    {
                        db.Execute(
                            $"UPDATE [{table}] Set [PageTypeId]=@PageTypeId, [SiteId]=@SiteId, [ParentId]=@ParentId, [SortOrder]=@SortOrder, [Title]=@Title, [NavigationTitle]=@NavigationTitle, [Slug]=@Slug, [IsHidden]=@IsHidden, [MetaKeywords]=@MetaKeywords, [MetaDescription]=@MetaDescription, [Route]=@Route, [Published]=@Published, [LastModified]=@LastModified WHERE [Id]=@Id",
                            page, transaction: tx);
                    }

                    // Map regions
                    foreach (var regionKey in currentRegions)
                    {
                        // Check that the region exists in the current model
                        if (HasRegion(model, regionKey))
                        {
                            var regionType = type.Regions.Single(r => r.Id == regionKey);

                            if (!regionType.Collection)
                            {
                                MapRegion(model, page, GetRegion(model, regionKey), regionType, regionKey, transaction: tx);
                            }
                            else
                            {
                                var sortOrder = 0;
                                foreach (var region in GetEnumerable(model, regionKey))
                                {
                                    MapRegion(model, page, region, regionType, regionKey, sortOrder++, transaction: tx);
                                }
                            }
                        }
                    }

                    if (tx != transaction)
                    {
                        tx.Commit();
                    }
                }
            }
            catch (Exception)
            {
                if (tx != transaction)
                {
                    tx.Rollback();
                }
                throw;
            }
        }

        /// <summary>
        /// Deletes the model with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <param name="transaction">The optional transaction</param>
        public void Delete(Guid id, IDbTransaction transaction = null)
        {
            db.Execute($"DELETE FROM [{table}] WHERE [Id]=@Id",
                new { Id = id }, transaction: transaction);

            if (cache != null)
                cache.Remove(id.ToString());
        }

        /// <summary>
        /// Deletes the given model.
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="transaction">The optional transaction</param>
        public void Delete<T>(T model, IDbTransaction transaction = null) where T : Models.Page<T>
        {
            Delete(model.Id, null);
        }



        #region Private get methods
        /// <summary>
        /// Loads the given data into a new model.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="page">The data entity</param>
        /// <returns>The page model</returns>
        private T Load<T>(Page page) where T : Models.Page<T>
        {
            var type = api.PageTypes.GetById(page.PageTypeId);

            if (type != null)
            {
                // Create an initalized model
                var model = (T)typeof(T).GetTypeInfo().GetMethod("Create", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke(null, new object[] { api, page.PageTypeId });
                var currentRegions = type.Regions.Select(r => r.Id).ToArray();

                // Map basic fields
                App.Mapper.Map<Page, Models.PageBase>(page, model);

                // Map page type route (if available)
                if (string.IsNullOrWhiteSpace(model.Route) && !string.IsNullOrEmpty(type.Route))
                    model.Route = type.Route;

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
                                if (region.Fields.Count == 1)
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
        /// Adds a simple single field value to a collection region.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="model">The model</param>
        /// <param name="regionId">The region id</param>
        /// <param name="field">The field</param>
        private void AddSimpleValue<T>(T model, string regionId, PageField field) where T : Models.Page<T>
        {
            if (model is Models.DynamicPage)
            {
                ((IList)((IDictionary<string, object>)((Models.DynamicPage)(object)model).Regions)[regionId])
                    .Add(DeserializeValue(field));
            }
            else
            {
                ((IList)model.GetType().GetTypeInfo().GetProperty(regionId, App.PropertyBindings).GetValue(model))
                    .Add(DeserializeValue(field));
            }
        }

        /// <summary>
        /// Sets the value of a simple single field region.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="model">The model</param>
        /// <param name="regionId">The region id</param>
        /// <param name="field">The field</param>
        private void SetSimpleValue<T>(T model, string regionId, PageField field) where T : Models.Page<T>
        {
            if (model is Models.DynamicPage)
            {
                ((IDictionary<string, object>)((Models.DynamicPage)(object)model).Regions)[regionId] = 
                    DeserializeValue(field);
            }
            else
            {
                model.GetType().GetTypeInfo().GetProperty(regionId, App.PropertyBindings).SetValue(model, DeserializeValue(field));
            }
        }

        /// <summary>
        /// Adds a complex region to a collection region.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="model">The model</param>
        /// <param name="regionId">The region id</param>
        /// <param name="fields">The field</param>
        private void AddComplexValue<T>(T model, string regionId, List<PageField> fields) where T : Models.Page<T>
        {
            if (model is Models.DynamicPage)
            {
                var list = (IList)((IDictionary<string, object>)((Models.DynamicPage)(object)model).Regions)[regionId];
                var obj = Models.DynamicPage.CreateRegion(api, model.TypeId, regionId);

                foreach (var field in fields)
                {
                    if (((IDictionary<string, object>)obj).ContainsKey(field.FieldId))
                    {
                        ((IDictionary<string, object>)obj)[field.FieldId] = DeserializeValue(field);
                    }
                }
                list.Add(obj);
            }
            else
            {
                var list = (IList)model.GetType().GetTypeInfo().GetProperty(regionId, App.PropertyBindings).GetValue(model);
                var obj = Activator.CreateInstance(list.GetType().GenericTypeArguments.First());

                foreach (var field in fields)
                {
                    var prop = obj.GetType().GetTypeInfo().GetProperty(field.FieldId, App.PropertyBindings);
                    if (prop != null)
                    {
                        prop.SetValue(obj, DeserializeValue(field));
                    }
                }
                list.Add(obj);
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
        private void SetComplexValue<T>(T model, string regionId, string fieldId, PageField field) where T : Models.Page<T>
        {
            if (model is Models.DynamicPage)
            {
                ((IDictionary<string, object>)((IDictionary<string, object>)((Models.DynamicPage)(object)model).Regions)[regionId])[fieldId] =
                    DeserializeValue(field);
            }
            else
            {
                var obj = model.GetType().GetTypeInfo().GetProperty(regionId, App.PropertyBindings).GetValue(model);
                if (obj != null)
                {
                    obj.GetType().GetTypeInfo().GetProperty(fieldId, App.PropertyBindings).SetValue(obj, DeserializeValue(field));
                }
            }
        }

        /// <summary>
        /// Deserializes the given field value.
        /// </summary>
        /// <param name="field">The page field</param>
        /// <returns>The value</returns>
        private object DeserializeValue(PageField field)
        {
            var type = App.Fields.GetByType(field.CLRType);

            if (type != null)
            {
                return JsonConvert.DeserializeObject(field.Value, type.Type);
            }
            return null;
        }
        #endregion

        #region private set methods
        /// <summary>
        /// Checks if the given model has a region with the specified id.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="model">The model</param>
        /// <param name="regionId">The region id</param>
        /// <returns>If the region exists</returns>
        private bool HasRegion<T>(T model, string regionId) where T : Models.Page<T>
        {
            if (model is Models.DynamicPage)
            {
                return ((IDictionary<string, object>)((Models.DynamicPage)(object)model).Regions).ContainsKey(regionId);
            }
            else
            {
                return model.GetType().GetTypeInfo().GetProperty(regionId, App.PropertyBindings) != null;
            }
        }

        /// <summary>
        /// Gets the region with the given key.
        /// </summary>
        /// <typeparam name="TModelType">The model type</typeparam>
        /// <param name="model">The model</param>
        /// <param name="regionId">The region id</param>
        /// <returns>The region</returns>
        private object GetRegion<T>(T model, string regionId) where T : Models.Page<T>
        {
            if (model is Models.DynamicPage)
            {
                return ((IDictionary<string, object>)((Models.DynamicPage)(object)model).Regions)[regionId];
            }
            else
            {
                return model.GetType().GetTypeInfo().GetProperty(regionId, App.PropertyBindings).GetValue(model);
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
                return region.GetType().GetTypeInfo().GetProperty(fieldId, App.PropertyBindings).GetValue(region);
            }
        }

        /// <summary>
        /// Gets the enumerator for the given region collection.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="model">The model</param>
        /// <param name="regionId">The region id</param>
        /// <returns>The enumerator</returns>
        private IEnumerable GetEnumerable<T>(T model, string regionId) where T : Models.Page<T>
        {
            object value = null;

            if (model is Models.DynamicPage)
            {
                value = ((IDictionary<string, object>)((Models.DynamicPage)(object)model).Regions)[regionId];
            }
            else
            {
                value = model.GetType().GetTypeInfo().GetProperty(regionId, App.PropertyBindings).GetValue(model);
            }

            if (value is IEnumerable)
                return (IEnumerable)value;
            return null;
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
        /// <param name="transaction">The optional transaction</param>
        private void MapRegion<T>(T model, Page page, object region, Models.RegionType regionType, string regionId, int sortOrder = 0, IDbTransaction transaction = null) where T : Models.Page<T>
        {
            var isNew = false;

            // Now map all of the fields
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
                        fieldValue = region;
                    }
                    else
                    {
                        // Get the field value for complex region
                        fieldValue = GetComplexValue(region, fieldDef.Id);
                    }

                    // check that the returned value matches the type specified for the page type
                    // otherwise deserialization won't work when the model is retrieved from the database
                    if (fieldValue.GetType() != fieldType.Type)
                        throw new ArgumentException("Given page field value does not match the configured page type");

                    // Check if we have the current field in the database already
                    var field = page.Fields
                        .SingleOrDefault(f => f.RegionId == regionId && f.FieldId == fieldDef.Id && f.SortOrder == sortOrder);

                    // if not, create a new field
                    if (field == null)
                    {
                        field = new PageField()
                        {
                            Id = Guid.NewGuid(),
                            PageId = page.Id,
                            RegionId = regionId,
                            FieldId = fieldDef.Id
                        };
                        page.Fields.Add(field);
                        isNew = true;
                    }

                    // Update field info & value
                    field.CLRType = fieldType.TypeName;
                    field.SortOrder = sortOrder;
                    field.Value = JsonConvert.SerializeObject(fieldValue);

                    // save the field
                    if (isNew)
                    {
                        db.Execute("INSERT INTO [Piranha_PageFields] ([Id], [PageId], [RegionId], [FieldId], [CLRType], [SortOrder], [Value]) VALUES(@Id, @PageId, @RegionId, @FieldId, @CLRType, @SortOrder, @Value)",
                            field, transaction: transaction);
                    }
                    else
                    {
                        db.Execute("UPDATE [Piranha_PageFields] Set [PageId]=@PageId, [RegionId]=@RegionId, [FieldId]=@FieldId, [CLRType]=@CLRType, [SortOrder]=@SortOrder, [Value]=@Value WHERE [Id]=@Id",
                            field, transaction: transaction);
                    }
                }
            }
        }
        #endregion

        #region Private helper methods
        /// <summary>
        /// Moves the pages around. This is done when a page is deleted or moved in the structure.
        /// </summary>
        /// <param name="parentId">The parent id</param>
        /// <param name="sortOrder">The sort order</param>
        /// <param name="increase">If sort order should be increase or decreased</param>
        /// <param name="transaction">The current transaction</param>
        private void MovePages(Guid? parentId, int sortOrder, bool increase, IDbTransaction transaction)
        {
            if (parentId.HasValue)
            {
                db.Execute($"UPDATE [{table}] SET [SortOrder]=[SortOrder] " + (increase ? "+ 1" : "- 1") +
                    " WHERE [ParentId]=@ParentId AND [SortOrder]>=@SortOrder",
                    new { ParentId = parentId.Value, SortOrder = sortOrder }, transaction: transaction);
            }
            else
            {
                db.Execute($"UPDATE [{table}] SET [SortOrder]=[SortOrder] " + (increase ? "+ 1" : "- 1") +
                    " WHERE [ParentId] IS NULL AND [SortOrder]>=@SortOrder",
                    new { SortOrder = sortOrder }, transaction: transaction);
            }
        } 
        #endregion
    }
}
