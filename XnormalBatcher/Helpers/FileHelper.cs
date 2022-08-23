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
        public static void CreateSubFolders(string root, List<string> subfolders)
        {
            foreach (string subFolder in subfolders.Where(subFolder => !Directory.Exists(root + subFolder)))
            {
                Directory.CreateDirectory(root + subFolder);
            }
        }
    }
}
