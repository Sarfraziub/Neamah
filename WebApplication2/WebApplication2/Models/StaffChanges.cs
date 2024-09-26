using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class StaffChanges
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Device ID")]
        public int DeviceId { get; set; }

        [Display(Name = "Staff ID")]
        public int StaffId { get; set; }

        [Display(Name = "Staff Name")]
        public string StaffName { get; set; }

        public string Department { get; set; }

        [Display(Name = "Changed By")]
        public string ChangedBy { get; set; }

        [Display(Name = "Date & Time")]
        public DateTime DateTime { get; set; }


        [Display(Name = "Note")]
        public string ChangeNote { get; set; }
    }
}