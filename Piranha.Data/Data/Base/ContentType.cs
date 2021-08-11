using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data.Data.Base
{
    /// <summary>
    /// Base class for creating content types.
    /// </summary>
    /// <typeparam name="T">The content type</typeparam>
    /// <typeparam name="TField">The field type</typeparam>
    public abstract class ContentType<T, TField>: IModel, IInternalId, ICreated, IModified
        where T : ContentType<T, TField>
        where TField : ContentTypeField<T>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ContentType()
        {
            Fields = new List<TField>();
        }

        #region Properties
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the display name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets the unique internal id.
        /// </summary>
        public string InternalId { get; set; }

        /// <summary>
        /// Gets/sets the optional description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets/sets the default route.
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Gets/sets the created date.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets/sets the last modification date.
        /// </summary>
        public DateTime LastModified { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Gets/sets the available fields.
        /// </summary>
        public IList<TField> Fields { get; set; }
        #endregion
    }
}
