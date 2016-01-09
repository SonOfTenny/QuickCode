using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace QuickCode.Models
{
    public class DowntimeType
    {
        [DisplayName("Downtime Type ID")]
        public int DowntimeTypeID { get; set; }
        [DisplayName("Downtime Type")]
        public string Name { get; set; }
    }
}