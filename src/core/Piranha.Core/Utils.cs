using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Piranha
{
    /// <summary>
    /// Utility methods
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Gets a subset of the given array as a new array.
        /// </summary>
        /// <typeparam name="T">The array type</typeparam>
        /// <param name="arr">The array</param>
        /// <param name="startpos">The startpos</param>
        /// <param name="length">The length</param>
        /// <returns></returns>
        public static T[] Subset<T>(this T[] arr, int startpos = 0, int length = 0)
        {
            List<T> tmp = new List<T>();

            length = length > 0 ? length : arr.Length - startpos;

            for (int i = 0; i < arr.Length; i++)
            {
                if (i >= startpos && i < (startpos + length))
                    tmp.Add(arr[i]);
            }

            return tmp.ToArray();
        }

        /// <summary>
        /// Implodes the string enumerable into a string.
        /// </summary>
        /// <param name="strs">The strings to implode</param>
        /// <param name="sep">The optional separator</param>
        /// <returns>The string</returns>
        public static string Implode(this IEnumerable<string> strs, string sep = "")
        {
            var sb = new StringBuilder();
            foreach (var str in strs)
            {
                if (sb.Length > 0 && sep != "")
                    sb.Append(sep);
                sb.Append(str);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Generates a slug from the given string.
        /// </summary>
        /// <param name="str">The string</param>
        /// <returns>The slug</returns>
        public static string GenerateSlug(string str)
        {
            var slug = Regex.Replace(str.ToLower()
                .Replace(" ", "-")
                .Replace("å", "a")
                .Replace("ä", "a")
                .Replace("á", "a")
                .Replace("à", "a")
                .Replace("ö", "o")
                .Replace("ó", "o")
                .Replace("ò", "o")
                .Replace("é", "e")
                .Replace("è", "e")
                .Replace("í", "i")
                .Replace("ì", "i"), @"[^a-z0-9-/]", "").Replace("--", "-");

            if (slug.EndsWith("-"))
                slug = slug.Substring(0, slug.LastIndexOf("-"));
            if (slug.StartsWith("-"))
                slug = slug.Substring(Math.Min(slug.IndexOf("-") + 1, slug.Length));
            return slug;
        }


        public static string GenerateETag(string name, DateTime date)
        {
            var encoding = new UTF8Encoding();

            using (var crypto = MD5.Create())
            {
                var str = name + date.ToString("yyyy-MM-dd HH:mm:ss");
                var bytes = crypto.ComputeHash(encoding.GetBytes(str));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
