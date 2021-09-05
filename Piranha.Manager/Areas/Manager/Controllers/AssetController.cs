using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Piranha.Areas.Manager.Controllers
{
    [Area("manager")]
    public class AssetController : Controller
    {
        #region members
        private Dictionary<string, string> contentTypes = new Dictionary<string, string>()
        {
            {".ico","image/x-icon" },
            {".png", "image/png" },
            {".css", "text/css" },
            {".js", "text/javascript" }
        };
        #endregion

        [Route("manager/assets/{*path}")]
        public IActionResult GetAsset(string path)
        {
            var assembly = typeof(AssetController).GetTypeInfo().Assembly;
            string[] result = assembly.GetManifestResourceNames();
            var filePath = "Piranha.Manager.assets." + path.Replace("/", ".");
            var stream = assembly.GetManifestResourceStream(filePath);

            if (stream != null)
                return new FileStreamResult(stream, GetContentType(path));
            return NotFound();
        }

        private string GetContentType(string path)
        {
            try
            {
                return contentTypes[path.Substring(path.LastIndexOf("."))];
            }
            catch { }
            return "text/plain";
        }
    }
}
