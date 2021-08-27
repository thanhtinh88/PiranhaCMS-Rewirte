using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Models
{
    /// <summary>
    /// An uploaded media file
    /// </summary>
    public sealed class MediaBase
    {
        #region Properties
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the filename.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets/sets the size of the uploaded asset.
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// Gets/sets the content type.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets/sets the public url.
        /// </summary>
        public string PublicUrl { get; set; }

        /// <summary>
        /// Gets/sets the created date.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets/sets the last modified date.
        /// </summary>
        public DateTime LastModified { get; set; }
        #endregion

    }
}
