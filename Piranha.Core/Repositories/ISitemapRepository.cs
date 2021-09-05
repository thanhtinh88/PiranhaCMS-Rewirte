using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Repositories
{
    public interface ISitemapRepository
    {
        IList<Models.SitemapItem> Get(bool onlyPublished = true);
    }
}
