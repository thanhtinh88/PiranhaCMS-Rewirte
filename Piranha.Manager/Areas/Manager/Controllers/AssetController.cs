using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
            var filePath = "Piranha.Manager.assets." + path.Replace("/", ".");
            var stream = assembly.GetManifestResourceStream(filePath);

            if (stream != null)
            {
                using (var reader = new BinaryReader(stream))
                {
                    return new FileContentResult(reader.ReadBytes((int)reader.BaseStream.Length), GetContentType(path));
                }
            }
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
