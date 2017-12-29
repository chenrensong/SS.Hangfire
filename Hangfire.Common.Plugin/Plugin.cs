using Hangfire.Plugin;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangfire.Common.Plugin
{
    public class Plugin : IHangfirePlugin
    {
        public void Register()
        {
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Test"), Cron.Daily);
        }

        public void UnRegister()
        {

        }
    }
}
