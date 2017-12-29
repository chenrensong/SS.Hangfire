using System;
using System.Collections.Generic;
using System.Text;

namespace Hangfire.Dashboard.Pages
{

    partial class BlockMetric
    {
        public BlockMetric(DashboardMetric dashboardMetric)
        {
            DashboardMetric = dashboardMetric;
        }

        public DashboardMetric DashboardMetric { get; }
    }
}
