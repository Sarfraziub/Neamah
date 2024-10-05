using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.ViewModel;

namespace WebApplication2.Models
{
    public class Device
    {
        [Key]
        [Display(Name = "Device ID")]
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Category { get; set; }


        //[Display(Name = "Is Rejected?")]
        //public bool IsRejected { get; set; }

        //[Display(Name = "Is Stored?")]
        //public bool IsStored { get; set; }

        //[Display(Name = "Is Reserved?")]
        //public bool IsReserved { get; set; }
        public StatusOption? Status { get; set; }


        public string DeviceOwner { get; set; }
        public DateTime CreatedAt { get; set; }
        [NotMapped]
        public Staff Staff { get; set; }


        [Display(Name = "Tag Number")]
        public string TagNumber { get; set; }
        public string Note { get; set; }

        [Display(Name = "Staff ID")]
        public int StaffId { get; set; }

        [Display(Name = "Staff Name")]
        public string StaffName { get; set; }
        public string Department { get; set; }
        public string DepartmentHead { get; set; }
        public string DepartmentHeadEmail { get; set; }
        public int CategoryId { get; set; }
        [NotMapped]
        public string AttachmentPath { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
    public enum StatusOption
    {
        [Display(Name = "Rejected")]
        IsRejected = 1,

        [Display(Name = "Stored")]
        IsStored = 2,

        [Display(Name = "Reserved")]
        IsReserved = 3
    }

}