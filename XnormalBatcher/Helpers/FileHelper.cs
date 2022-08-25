using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnormalBatcher.Helpers
{
    internal static class FileHelper
    {
        public static string[] SubFolders = new string[] { @"LowPoly\", @"HighPoly\", @"Cage\", @"Maps\" };
        
        public static void CreateSubFolders(string root)
        {
            foreach (string subFolder in SubFolders.Where(subFolder => !Directory.Exists(root + subFolder)))
            {
                Directory.CreateDirectory(root + subFolder);
            }
        }
        /// <summary>
        /// Parse folder for files with a specific suffix/prefix and extensions.
        /// </summary>
        /// <param name="folder">string Path to folder</param>
        /// <param name="ext">string extension to use (without the '.')</param>
        /// <param name="suffix">string suffix(or prefix depending on app setting) to look for</param>
        /// <returns></returns>
        public static string[] GetItems(string folder, string ext, string suffix = "", bool UseTermsAsPrefix = false)
        {
            string test;
            string[] r = { };
            if (Directory.Exists(folder))
            {
                test = UseTermsAsPrefix ? $"{suffix}*.{ext}" : $"*{suffix}.{ext}";
                r = Directory.GetFiles(folder, test);
            }
            return r;
        }
        /// <summary>
        /// Create Suffix / Prefix with it's separator as a string placed correctly
        /// </summary>
        /// <param name="term"></param>
        /// <returns>Constructed string with separator</returns>
        public static string CreateSuffix(string term, string separator, bool UseTermsAsPrefix = false)
        {            
            if (separator == "\" \"")
                separator = " ";
            return UseTermsAsPrefix ? term + separator : separator + term;
        }

        public static string GenerateExtensionFilter(string title, List<string> extensions)
        {
            var list = string.Join(", ", extensions.Select(e => $"*.{e}"));
            return $"{title}({list})|{list}";
        }
    }
}
