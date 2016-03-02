using QuickCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickCode.ViewModels
{
    public class UserViewModel 
    {
        //  Add the new name properties:
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        // user properties you'd like to edit here

    }
}