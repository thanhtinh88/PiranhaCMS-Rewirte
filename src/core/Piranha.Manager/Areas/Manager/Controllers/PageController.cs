﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class PageController : Controller
    {
        #region Members
        /// <summary>
        /// The current api.
        /// </summary>
        private readonly IApi api;
        #endregion

        public PageController(IApi api)
        {
            this.api = api;
        }

        /// <summary>
        /// Gets the list view for the pages.
        /// </summary>
        [Route("manager/pages")]
        public IActionResult List()
        {
            return View(Models.PageListModel.Get(api));
        }

        /// <summary>
        /// Gets the edit view for a page.
        /// </summary>
        /// <param name="id">The page id</param>
        [Route("manager/page/{id:Guid}")]
        public IActionResult Edit(Guid id)
        {
            return View(Models.PageEditModel.GetById(api, id));
        }

        /// <summary>
        /// Adds a new page of the given type.
        /// </summary>
        /// <param name="type">The page type id</param>
        [Route("manager/page/add/{type}")]
        public IActionResult Add(string type)
        {
            var sitemap = api.Sitemap.Get(false);
            var model = Models.PageEditModel.Create(type);
            model.SortOrder = sitemap.Count;

            return View("Edit", model);
        }

        /// <summary>
        /// Saves the given page model
        /// </summary>
        /// <param name="model">The page model</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("manager/page/save")]
        public IActionResult Save(Models.PageEditModel model)
        {
            if (model.Save(api))
                return RedirectToAction("List");
            return View(model);
        }

        /// <summary>
        /// Saves and publishes the given page model.
        /// </summary>
        /// <param name="model">The page model</param>
        public IActionResult Publish(Models.PageEditModel model)
        {
            if (model.Save(api, true))
                return RedirectToAction("List");
            return View(model);
        }
    }
}
