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
        public IEnumerable<FiscalWeek> fiscalWeek { get; set; }
    }
    public class ProductionVM
    {
        public DateTime FiscalWeek { get; set; }
        public int FiscalWeekNo { get; set; }
        public int LastFiscalWeekNo { get; set; }
        public DateTime LastFiscalWeek { get; set; }
        public string Last2FiscalWeeks { get; set; }
        public DateTime FutreFiscalWeek { get; set; }
        public int FutureFiscalWeekNo { get; set; }
        public IEnumerable<Downtime> Downtime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string PlantName { get; set; }
        /// <summary>
        /// BATCH
        /// </summary>
        // total waste setters/getters 
        public double BatchTotalCrumb { get; set; }
        public double BatchTotalComp { get; set; }
        public double BatchTotalPack { get; set; }
        public double BatchTotalGenPack { get; set; }
        // total mixes (Actual Mix)
        public double BatchTotalMixes { get; set; }
        // total production time
        public double BatchTotalTime { get; set; }
        // total Manning
        public double BatchTotalManning { get; set; }
        // total Downtime (mins)
        public double BatchTotalDowntime { get; set; }

        /// <summary>
        /// PAN
        /// </summary>
        public double PanTotalCrumb { get; set; }
        public double PanTotalComp { get; set; }
        public double PanTotalPack { get; set; }
        public double PanTotalGenPack { get; set; }
        // total mixes (Actual Mix)
        public double PanTotalMixes { get; set; }
        // total production time
        public double PanTotalTime { get; set; }
        // total Manning
        public double PanTotalManning { get; set; }
        // total Downtime (mins)
        public double PanTotalDowntime { get; set; }

        /// <summary>
        /// ROLL
        /// </summary>
        public double RollTotalCrumb { get; set; }
        public double RollTotalComp { get; set; }
        public double RollTotalPack { get; set; }
        public double RollTotalGenPack { get; set; }
        // total mixes (Actual Mix)
        public double RollTotalMixes { get; set; }
        // total production time
        public double RollTotalTime { get; set; }
        // total Manning
        public double RollTotalManning { get; set; }
        // total Downtime (mins)
        public double RollTotalDowntime { get; set; }

        /// <summary>
        /// WHEATEN
        /// </summary>
        // total waste setters/getters 
        public double WheatenTotalCrumb { get; set; }
        public double WheatenTotalComp { get; set; }
        public double WheatenTotalPack { get; set; }
        public double WheatenTotalGenPack { get; set; }
        // total mixes (Actual Mix)
        public double WheatenTotalMixes { get; set; }
        // total production time
        public double WheatenTotalTime { get; set; }
        // total Manning
        public double WheatenTotalManning { get; set; }
        // total Downtime (mins)
        public double WheatenTotalDowntime { get; set; }

        /// <summary>
        /// SODA
        /// </summary>
        public double SodaTotalCrumb { get; set; }
        public double SodaTotalComp { get; set; }
        public double SodaTotalPack { get; set; }
        public double SodaTotalGenPack { get; set; }
        // total mixes (Actual Mix)
        public double SodaTotalMixes { get; set; }
        // total production time
        public double SodaTotalTime { get; set; }
        // total Manning
        public double SodaTotalManning { get; set; }
        // total Downtime (mins)
        public double SodaTotalDowntime { get; set; }

        /// <summary>
        /// PANCAKES
        /// </summary>
        public double PancakesTotalCrumb { get; set; }
        public double PancakesTotalComp { get; set; }
        public double PancakesTotalPack { get; set; }
        public double PancakesTotalGenPack { get; set; }
        // total mixes (Actual Mix)
        public double PancakesTotalMixes { get; set; }
        // total production time
        public double PancakesTotalTime { get; set; }
        // total Manning
        public double PancakesTotalManning { get; set; }
        // total Downtime (mins)
        public double PancakesTotalDowntime { get; set; }

        /// <summary>
        /// POTATO
        /// </summary>
        public double PotatoTotalCrumb { get; set; }
        public double PotatoTotalComp { get; set; }
        public double PotatoTotalPack { get; set; }
        public double PotatoTotalGenPack { get; set; }
        // total mixes (Actual Mix)
        public double PotatoTotalMixes { get; set; }
        // total production time
        public double PotatoTotalTime { get; set; }
        // total Manning
        public double PotatoTotalManning { get; set; }
        // total Downtime (mins)
        public double PotatoTotalDowntime { get; set; }
    }
    public class DowntimeVM
    {
        // total Downtime (mins)
        public double TotalDowntime { get; set; }

    }
}