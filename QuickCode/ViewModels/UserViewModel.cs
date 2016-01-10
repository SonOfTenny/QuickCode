using QuickCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickCode.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<User>Users { get; set; }
        
    }
}