using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hangfire.Plugin
{
    public interface IHangfirePlugin
    {
        /// <summary>
        /// 注册
        /// </summary>
        void Register();

        /// <summary>
        /// 反注册
        /// </summary>
        void UnRegister();
    }
}
