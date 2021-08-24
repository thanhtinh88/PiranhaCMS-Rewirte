using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Server
{
    /// <summary>
	/// Interface for creating storage sessions.
	/// </summary>
    public interface IStorageSession
    {
        /// <summary>
		/// Opens a new storage session.
		/// </summary>
		/// <returns>An open storage session</returns>
        IStorage Open();
    }
}
