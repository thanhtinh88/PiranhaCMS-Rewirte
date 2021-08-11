using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data.Data
{
    /// <summary>
    /// Post fields hold the actual content of the posts.
    /// </summary>
    public sealed class PostField: Base.ContentField<Post, PostType, PostTypeField>
    {
    }
}
