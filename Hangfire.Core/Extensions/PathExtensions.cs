using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hangfire.Extensions
{
    public static class PathExtensions
    {
        private static string DirectorySeparatorChar = Path.DirectorySeparatorChar.ToString();


        /// <summary>
        　　/// 获取文件绝对路径
        　　/// </summary>
        　　/// <param name="path">文件路径</param>
        　　/// <returns></returns>
        public static string MapPath(this string path, string contentRootPath)
        {
            return IsAbsolute(path) ? path : Path.Combine(contentRootPath, path.TrimStart('~', '/').Replace("/", DirectorySeparatorChar));
        }

        /// <summary>
        /// 是否是绝对路径
        /// windows下判断 路径是否包含 ":"
        /// Mac OS、Linux下判断 路径是否包含 "\"
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static bool IsAbsolute(this string path)
        {
            return Path.VolumeSeparatorChar == ':' ? path.IndexOf(Path.VolumeSeparatorChar) > 0 : path.IndexOf('\\') > 0;
        }
    }
}
