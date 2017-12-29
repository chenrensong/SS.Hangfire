using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Hangfire.Plugin
{
    internal class PluginService
    {

        /// <summary>
        /// Plugins<br/>
        /// 插件列表<br/>
        /// </summary>
        public static IList<PluginInfo> Plugins { get; protected set; } = new List<PluginInfo>();

        /// <summary>
        /// 获取Plugin里面的Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetType(string type)
        {
            var clazzName = type.Split(',')[0];
            var assemblyName = type.Substring(clazzName.Length + 1).Trim();
            var plugin = Plugins.FirstOrDefault(m => m.Assembly.FullName == assemblyName);
            var newAssemebly = plugin.Assembly.GetTypes().FirstOrDefault(m => m.AssemblyQualifiedName == type);
            return newAssemebly;
        }

        public static void Register(List<string> pathList)
        {
            foreach (var path in pathList)
            {
                Register(path);
            }
        }

        public static void Register(string path)
        {
            var pluginInfo = PluginInfo.FromDirectory(path);
            Plugins.Add(pluginInfo);
            var types = pluginInfo.Assembly.GetTypes().Where(t => t.IsPublic & typeof(IHangfirePlugin).IsAssignableFrom(t));
            foreach (var type in types)
            {
                MethodInfo method = type.GetMethod("Register");
                object obj = Activator.CreateInstance(type);
                method.Invoke(obj, null);
            }
        }
    }
}
