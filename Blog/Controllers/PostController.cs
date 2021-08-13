using Microsoft.AspNetCore.Mvc;
using Piranha.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            if (Request.Query["id"].Count == 1)
            {
                try
                {
                    var id = new Guid(Request.Query["id"][0]);
                    using (var api = new Api())
                    {
                        return View(api.Posts.GetById(id));
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return null;
        }
    }
}
