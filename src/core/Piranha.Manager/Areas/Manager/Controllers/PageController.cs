﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class PageController : ManagerAreaControllerBase
    {
        public PageController(IApi api) : base (api) { }

        /// <summary>
        /// Gets the list view for the pages.
        /// </summary>
        [Route("manager/pages")]
        public ViewResult List()
        {
            return View(Models.PageListModel.Get(_api));
        }

        /// <summary>
        /// Gets the edit view for a page.
        /// </summary>
        /// <param name="id">The page id</param>
        [Route("manager/page/{id:Guid}")]
        public IActionResult Edit(Guid id)
        {
            return View(Models.PageEditModel.GetById(_api, id));
        }

        /// <summary>
        /// Adds a new page of the given type.
        /// </summary>
        /// <param name="type">The page type id</param>
        [Route("manager/page/add/{type}")]
        public IActionResult Add(string type)
        {
            var sitemap = _api.Sitemap.Get(false);
            var model = Models.PageEditModel.Create(type);
            model.SortOrder = sitemap.Count;

            return View("Edit", model);
        }

        /// <summary>
        /// Saves the given page model
        /// </summary>
        /// <param name="model">The page model</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("manager/page/save")]
        public IActionResult Save(Models.PageEditModel model)
        {
            if (model.Save(_api))
                return RedirectToAction("List");
            return View(model);
        }

        /// <summary>
        /// Saves and publishes the given page model.
        /// </summary>
        /// <param name="model">The page model</param>
        [HttpPost]
        [Route("manager/page/publish")]
        public IActionResult Publish(Models.PageEditModel model)
        {
            if (model.Save(_api, true))
                return RedirectToAction("List");
            return View(model);
        }

        /// <summary>
        /// Saves and unpublishes the given page model.
        /// </summary>
        /// <param name="model">The page model</param>
        [HttpPost]
        [Route("manager/page/unpublish")]
        public IActionResult UnPublish(Models.PageEditModel model)
        {
            if (model.Save(_api, false))
            {
                return RedirectToAction("List");
            }

            return View(model);
        }

        /// <summary>
        /// Moves a page to match the given structure.
        /// </summary>
        /// <param name="structure">The page structure</param>
        [HttpPost]
        [Route("manager/page/move")]
        public IActionResult Move([FromBody]Models.PageStructureModel structure)
        {
            for (int n = 0; n < structure.Items.Count; n++)
            {
                var moved = MovePage(structure.Items[n], n);
                if (moved)
                    break;
            }
            return View("Partial/_Sitemap", _api.Sitemap.Get(false));
        }

        /// <summary>
        /// Deletes the page with the given id.
        /// </summary>
        /// <param name="id">The unique id</param>
        [Route("manager/page/delete/{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            _api.Pages.Delete(id);
            return RedirectToAction("List");
        }

        #region private methods
        private bool MovePage(Models.PageStructureModel.PageStructureItem page, int sortOrder = 1, Guid? parentId = null)
        {
            var model = _api.Pages.GetById(page.Id);
            if (model != null)
            {
                if (model.ParentId != parentId || model.SortOrder != sortOrder)
                {
                    // Move the page in the structure
                    _api.Pages.Move(model, parentId, sortOrder);

                    // We only move one page at a time so we're done
                    return true;
                }

                for (int n = 0; n < page.Children.Count; n++)
                {
                    var moved = MovePage(page.Children[n], n, page.Id);
                    if (moved)
                        return true;
                }
            }
            return false;
        }
        #endregion
    }
}
