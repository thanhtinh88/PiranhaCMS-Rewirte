using Piranha.Core.Models;
using System.Collections.Generic;

namespace Piranha.Core.Repositories
{
    /// <summary>
	/// The client site map repository interface.
	/// </summary>
    public interface ISiteMapRepository
    {
        /// <summary>
		/// Gets the current sitemap.
		/// </summary>
		/// <returns>The hierarchical sitemap</returns>
        IList<SiteMapModel> Get();
    }
}