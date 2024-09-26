using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.ViewModel
{
    public class DeviceEditViewModel
    {
        public Device Device { get; set; }
        public IEnumerable<StaffChanges> StaffChanges { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}