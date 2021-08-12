using System;

namespace Piranha.Core.Extend
{
    /// <summary>
    /// The different extension types available.
    /// </summary>
    [Flags]
    public enum ExtensionType
    {
        Region = 1,
        Property = 2,
        Author = 4,
        Category = 8,
        Media = 16,
        MediaFolder = 32,
        Page = 64,
        PageType = 128,
        Param = 256,
        Post = 512,
        PostType = 1024,
        Tag = 2048
    }
}