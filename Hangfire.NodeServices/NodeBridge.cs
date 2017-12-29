using Hangfire.Plugin;
using Hangfire.Server;
using Microsoft.AspNetCore.NodeServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hangfire.Console;
namespace Hangfire.NodeServices
{
    public class Test
    {
        public static async Task Run(PerformContext context)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(i);
            }
            int j = 0;
            var progressBar = context.WriteProgressBar();
            foreach (var item in list.WithProgress(progressBar))
            {
                progressBar.SetValue((j * 1.0) / list.Count * 100);
                await Task.Delay(100);
                j++;
            }
            var options = new { width = 400, height = 200, showArea = true, showPoint = true, fullWidth = true };
            var data = new
            {
                labels = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" },
                series = new[] {
                    new[] { 1, 5, 2, 5, 4, 3 },
                    new[] { 2, 3, 4, 8, 1, 2 },
                    new[] { 5, 4, 3, 2, 1, 0 }
                }
            };
            var result = await NodePluginBridge.Instance.InvokeAsync<string>("./App_Data/Plugin/Hangfire.NodeServices/Node/renderChart", "line", options, data);
            context.WriteLine(result);
        }
    }

    public class Plugin : IHangfirePlugin
    {
        public void Register()
        {
            RecurringJob.AddOrUpdate(() => Test.Run(null), Cron.Daily);
        }

        public void UnRegister()
        {

        }
    }
}
