﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Models
{
    public class Tag
    {
        #region Properties
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the slug.
        /// </summary>
        public string Slug { get; set; }
        #endregion
    }
}
