﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Manager
{
    public class ResourceMiddleware
    {
        #region Members
        /// <summary>
        /// The next middleware in the pipeline.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// The currently embedded asset types.
        /// </summary>
        private readonly Dictionary<string, string> contentTypes = new Dictionary<string, string>()
        {
            {".ico", "image/x-icon" },
            {".png", "image/png" },
            {".css", "text/css" },
            {".js", "text/javascript" }
        };
        #endregion

        public ResourceMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The current http context</param>
        /// <returns>An async task</returns>
        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value;
            if (path.StartsWith("/manager/assets"))
            {
                var provider = new EmbeddedFileProvider(Module.Assembly);
                var fileInfo = provider.GetFileInfo(path.Replace("/manager/", ""));
                
                if (fileInfo.Exists)
                {
                    var headers = context.Response.GetTypedHeaders();
                    context.Response.ContentType = GetContentType(Path.GetExtension(path));
                    context.Response.ContentLength = fileInfo.Length;
                    headers.LastModified = fileInfo.LastModified.ToUniversalTime();

                    await context.Response.SendFileAsync(fileInfo);
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
            }
            else
            {
                await next.Invoke(context);
            }
        }

        #region Private methods
        /// <summary>
        /// Gets the content type for the asset.
        /// </summary>
        /// <param name="path">The asset path</param>
        /// <returns>The content type</returns>
        private string GetContentType(string path)
        {
            try
            {
                return contentTypes[path.Substring(path.LastIndexOf("."))];
            }
            catch { }
            return "text/plain";
        }
        #endregion
    }
}
