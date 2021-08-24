namespace Piranha.Extend
{
    /// <summary>
    /// Interface for fields.
    /// </summary>
    public interface IField
    {
        /// <summary>
        /// Gets the client value.
        /// </summary>
        /// <returns></returns>
        object GetValue();

        /// <summary>
        /// Initializes the field for client use.
        /// </summary>
        void Init();

        /// <summary>
        /// Initializes the field for manager use. This
        /// method can be used for loading additional meta
        /// data needed.
        /// </summary>
        void InitManager();
    }
}