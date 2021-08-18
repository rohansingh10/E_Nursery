using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace E_Nursery.Models
{
    public class OurDbContext : DbContext
    {
        public OurDbContext() : base("dbcon")
        {
        } 
        public DbSet<UserAccount> userAccount { get; set; }
        public DbSet<NurseryAccount> nurseryAccount { get; set; }
        public DbSet<AdminAccount> adminAccount { get; set; }
        public DbSet<Feedback> feedback { get; set; }
        public DbSet<Article> article { get; set; }
        public DbSet<ANotification> notifications { get; set; }

        public System.Data.Entity.DbSet<E_Nursery.Models.NurseryInventory> NurseryInventories { get; set; }
    }
}