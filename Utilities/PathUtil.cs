using System;
using System.IO;

namespace Utilities
{
    public static class PathUtil
    {
        public static string GetPathToFile(string fileRelativePath) =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileRelativePath);
    }
}
