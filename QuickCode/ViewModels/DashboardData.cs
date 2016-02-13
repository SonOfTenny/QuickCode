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
    public class ProductionVM
    {
        /// <summary>
        /// BATCH
        /// </summary>
        // total waste setters/getters 
        public int BatchTotalCrumb { get; set; }
        public int BatchTotalComp { get; set; }
        public int BatchTotalPack { get; set; }
        public int BatchTotalGenPack { get; set; }
        // total mixes (Actual Mix)
        public int BatchTotalMixes { get; set; }
        // total production time
        public double BatchTotalTime { get; set; }
        // total Manning
        public int BatchTotalManning { get; set; }
        // total Downtime (mins)
        public double BatchTotalDowntime { get; set; }

        /// <summary>
        /// PAN
        /// </summary>
        public int PanTotalCrumb { get; set; }
        public int PanTotalComp { get; set; }
        public int PanTotalPack { get; set; }
        public int PanTotalGenPack { get; set; }
        // total mixes (Actual Mix)
        public int PanTotalMixes { get; set; }
        // total production time
        public double PanTotalTime { get; set; }
        // total Manning
        public int PanTotalManning { get; set; }
        // total Downtime (mins)
        public double PanTotalDowntime { get; set; }

        /// <summary>
        /// ROLL
        /// </summary>
        public int RollTotalCrumb { get; set; }
        public int RollTotalComp { get; set; }
        public int RollTotalPack { get; set; }
        public int RollTotalGenPack { get; set; }
        // total mixes (Actual Mix)
        public int RollTotalMixes { get; set; }
        // total production time
        public double RollTotalTime { get; set; }
        // total Manning
        public int RollTotalManning { get; set; }
        // total Downtime (mins)
        public double RollTotalDowntime { get; set; }

        /// <summary>
        /// WHEATEN
        /// </summary>
        // total waste setters/getters 
        public int WheatenTotalCrumb { get; set; }
        public int WheatenTotalComp { get; set; }
        public int WheatenTotalPack { get; set; }
        public int WheatenTotalGenPack { get; set; }
        // total mixes (Actual Mix)
        public int WheatenTotalMixes { get; set; }
        // total production time
        public double WheatenTotalTime { get; set; }
        // total Manning
        public int WheatenTotalManning { get; set; }
        // total Downtime (mins)
        public double WheatenTotalDowntime { get; set; }

        /// <summary>
        /// SODA
        /// </summary>
        public int SodaTotalCrumb { get; set; }
        public int SodaTotalComp { get; set; }
        public int SodaTotalPack { get; set; }
        public int SodaTotalGenPack { get; set; }
        // total mixes (Actual Mix)
        public int SodaTotalMixes { get; set; }
        // total production time
        public double SodaTotalTime { get; set; }
        // total Manning
        public int SodaTotalManning { get; set; }
        // total Downtime (mins)
        public double SodaTotalDowntime { get; set; }

        /// <summary>
        /// PANCAKES
        /// </summary>
        public int PancakesTotalCrumb { get; set; }
        public int PancakesTotalComp { get; set; }
        public int PancakesTotalPack { get; set; }
        public int PancakesTotalGenPack { get; set; }
        // total mixes (Actual Mix)
        public int PancakesTotalMixes { get; set; }
        // total production time
        public double PancakesTotalTime { get; set; }
        // total Manning
        public int PancakesTotalManning { get; set; }
        // total Downtime (mins)
        public double PancakesTotalDowntime { get; set; }

        /// <summary>
        /// POTATO
        /// </summary>
        public int PotatoTotalCrumb { get; set; }
        public int PotatoTotalComp { get; set; }
        public int PotatoTotalPack { get; set; }
        public int PotatoTotalGenPack { get; set; }
        // total mixes (Actual Mix)
        public int PotatoTotalMixes { get; set; }
        // total production time
        public double PotatoTotalTime { get; set; }
        // total Manning
        public int PotatoTotalManning { get; set; }
        // total Downtime (mins)
        public double PotatoTotalDowntime { get; set; }
    }
    public class DowntimeVM
    {
        // total Downtime (mins)
        public double TotalDowntime { get; set; }

    }
}