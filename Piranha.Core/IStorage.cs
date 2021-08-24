using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Piranha
{
    /// <summary>
	/// Interface for handling the main storage manager.
	/// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Opens a new storage session.
        /// </summary>
        /// <returns>A new open session</returns>
        Task<IStorageSession> OpenAsync();
    }
}
