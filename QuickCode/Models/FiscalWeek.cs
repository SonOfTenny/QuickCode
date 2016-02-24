using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickCode.Models
{
    public class FiscalWeek
    {
        public int FiscalWeekID { get; set; }
        public int FiscalYear { get; set; }
        public string WeekName { get; set; }
        public int WeekQuarter { get; set; }
        public int WeekMonth { get; set; }
        public int WeekNumber { get; set; }
        public DateTime WeekStart { get; set; }
    }
}