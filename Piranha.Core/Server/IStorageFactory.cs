using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Core.Server
{
    /// <summary>
	/// Interface for creating storage sessions.
	/// </summary>
    public interface IStorageFactory
    {
        /// <summary>
		/// Opens a new storage session.
		/// </summary>
		/// <returns>An open storage session</returns>
        IStorage Open();
    }
}
