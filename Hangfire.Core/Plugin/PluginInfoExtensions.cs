using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hangfire.Plugin
{
    /// <summary>
    /// Plugin information extension methods<br/>
    /// 插件信息的扩展函数<br/>
    /// </summary>
    public static class PluginInfoExtensions
    {
        /// <summary>
        /// Get directory name<br/>
        /// 获取目录名称<br/>
        /// </summary>
        /// <param name="info">Plugin information</param>
        /// <returns></returns>
        public static string DirectoryName(this PluginInfo info)
        {
            return Path.GetFileName(info.Directory);
        }

        /// <summary>
        /// Get source directory<br/>
        /// 获取源代码目录的路径<br/>
        /// </summary>
        /// <param name="info">Plugin information</param>
        /// <returns></returns>
        public static string SourceDirectory(this PluginInfo info)
        {
            return Path.Combine(info.Directory, "src");
        }

        /// <summary>
        /// Get assembly file path<br/>
        /// 获取程序集文件的路径<br/>
        /// </summary>
        /// <param name="info">Plugin information</param>
        /// <returns></returns>
        public static string AssemblyPath(this PluginInfo info)
        {
            return Path.Combine(info.Directory, $"{info.DirectoryName()}.dll");
        }

        /// <summary>
        /// Get plugin version objects<br/>
        /// Return an empty version if parse failed<br/>
        /// 获取插件的版本对象<br/>
        /// 如果解析失败则返回空版本<br/>
        /// </summary>
        /// <param name="info">Plugin information</param>
        /// <returns></returns>
        public static Version VersionObject(this PluginInfo info)
        {
            Version version;
            if (Version.TryParse(info.Version.Split(' ')[0], out version))
            {
                return version;
            }
            return new Version(0, 0, 0);
        }

        /// <summary>
        /// Get assembly path from plugin's reference directory<br/>
        /// Return null if not found<br/>
        /// 从插件的引用目录获取引用程序集的路径<br/>
        /// 如果找不到则返回null<br/>
        /// </summary>
        /// <param name="info">Plugin information</param>
        /// <param name="assemblyName">Assembly name</param>
        /// <returns></returns>
        public static string ReferenceAssemblyPath(this PluginInfo info, string assemblyName)
        {
            var paths = new List<string>() {
                Path.Combine(info.Directory, "references", $"{assemblyName}.dll")
            };
            foreach (var path in paths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }
            return null;
        }


    }
}
