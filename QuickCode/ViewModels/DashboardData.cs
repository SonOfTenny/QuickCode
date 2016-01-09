using QuickCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickCode.ViewModels
{
    public class DashboardData
    {
        public IEnumerable<Production> Production { get; set; }
        public IEnumerable<Downtime> Downtime { get; set; }
        public IEnumerable<Production> MonthlyProduction { get; set; }
        public IEnumerable<Downtime> MonthlyDowntime { get; set; }
    }
}