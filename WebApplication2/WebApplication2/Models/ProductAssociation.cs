using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ProductAssociation
    {
        [Key]
        public int AssociationId { get; set; }
        public DateTime AssignDate { get; set; }
        public bool IsAssigned { get; set; }
        public int UserId { get; set; }
        public virtual Users User { get; set; }
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
    }
}