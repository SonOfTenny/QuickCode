using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuickCode.Models
{
    public class Production
    {
        [DisplayName("Production ID")]
        public int ProductionID { get; set; }
        [DisplayName("User")]
        public string UserID { get; set; }
        [DisplayName("Shift Type")]
        public int ShiftID { get; set; }
        [DisplayName("Plant Name")]
        public int PlantID { get; set; }
        [DisplayName("Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0: HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
        [DisplayName("End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0: HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
        [DisplayName("Actual Mix")]
        public double ActualMix { get; set; }
        [DisplayName("Crumb Waste")]
        public double CrumbWaste { get; set; }
        [DisplayName("Compactor Waste")]
        public double Cmp_Waste { get; set; }
        [DisplayName("Packaging Waste")]
        public double Pack_Waste { get; set; }
        [DisplayName("General Packaging Waste")]
        public double Gen_Pack_Waste { get; set; }
        [DisplayName("Standard Manning")]
        public double StdManning { get; set; }
        [DisplayName("Agency Manning")]
        public double AgencyManning { get; set; }
        [DisplayName("Operator Manning")]
        public double OpManning { get; set; }
        public double Manning { get; set; }
        public string Notes { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public double TotalWaste { get; set; }
        public double TotalProdMins { get; set; }

        // all the fancy foreign keys
        public virtual User User { get; set; }
        public virtual Shift Shift { get; set; }
        public virtual Plant Plant { get; set; }

    }
}