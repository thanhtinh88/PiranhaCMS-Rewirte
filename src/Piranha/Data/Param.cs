﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data
{
    /// <summary>
    /// String parameter.
    /// </summary>
    public sealed class Param : Param<string> 
    {
    }

    /// <summary>
    /// Generic system parameter.
    /// </summary>
    /// <typeparam name="T">The value type</typeparam>
    public class Param<T>:IModel, ICreated, IModified
    {
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the unique key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets/sets the value.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets/sets the optional description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets/sets the created date.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets/sets the last modification date.
        /// </summary>
        public DateTime LastModified { get; set; }
    }
}
