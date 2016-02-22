using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuickCode.Models
{
    public class AccessTypes
    {
        [Key]
        public int AccessID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       
    }
    public class UserAccessTypes
    {
        [Key]
        public int UserAccessID { get; set; }
        [DisplayName("Username")]
        public string UserID { get; set; }
        [DisplayName("Access Type")]
        public int AccessID { get; set; }
        // all the fancy foreign keys
        public virtual User User { get; set; }
        public virtual AccessTypes Access { get; set; }
    }
}