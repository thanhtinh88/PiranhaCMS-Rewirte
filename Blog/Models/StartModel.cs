
using Piranha.Core.Models;

namespace Blog.Models
{
    /// <summary>
    /// The start page model.
    /// </summary>
    public class StartModel: PageModel
    {
        /// <summary>
        /// Gets/sets the startpage blog archive.
        /// </summary>
        public ArchiveModel Archive { get; set; }
    }
}
