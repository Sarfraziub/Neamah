using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.ViewModel
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("ConnectionString")
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<ProductAssociation> ProductAssociations { get; set; }
        public DbSet<StaffChanges> StaffChanges { get; set; }
    }
}