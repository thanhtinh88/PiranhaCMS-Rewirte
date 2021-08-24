using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF.Data
{
    /// <summary>
    /// Tags are used to classify content.
    /// </summary>
    public sealed class Tag: IModel, ISlug, ICreated, IModified
    {
        #region Properties
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        #endregion
    }
}
