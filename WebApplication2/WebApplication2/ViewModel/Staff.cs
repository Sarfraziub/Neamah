﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModel
{
    public class Staff
    {
        [Display(Name = "Staff Id")]
        public int staff_id { get; set; }
        [Display(Name = "Name")]
        public string staff_name_en { get; set; }
        [Display(Name = "Department")]
        public string department_en { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
    }
}