using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Repositories
{
    public interface ISitemapRepository
    {
        /// <summary>
        /// Gets the hierachical sitemap structure.
        /// </summary>
        /// <param name="onlyPublished">If only published items should be included</param>
        /// <returns>The sitemap</returns>
        IList<Models.SitemapItem> Get(bool onlyPublished = true);
    }
}
