using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data.Data.Base
{
    /// <summary>
    /// Base class for creating content fields.
    /// </summary>
    /// <typeparam name="T">The content type</typeparam>
    /// <typeparam name="TType">The content type type</typeparam>
    /// <typeparam name="TFieldType">The field type type<</typeparam>
    public abstract class ContentField<T, TType, TFieldType> : IModel
        where TType: ContentType<TType, TFieldType>
        where TFieldType: ContentTypeField<TType>
    {
        #region Properties
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the field type.
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Gets/sets the parent id.
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// Gets/sets the field value.
        /// </summary>
        public string Value { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Gets/sets the field type.
        /// </summary>
        public TFieldType Type { get; set; }

        /// <summary>
        /// Gets/sets the parent.
        /// </summary>
        public T Parent { get; set; }
        #endregion
    }
}
