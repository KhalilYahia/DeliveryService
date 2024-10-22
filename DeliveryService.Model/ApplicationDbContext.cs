
using DeliveryService.Data.SeedData;
using DeliveryService.Domain.Entities;
using DeliveryService.Model.Configuration;
using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Inerfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using DeliveryService.Data;
using DeliveryService.Model.Configuration;

namespace DeliveryService.Model
{
    public class ApplicationDbContext:IdentityDbContext<CustomUser,CustomRole,string>// DbContext /*IdentityDbContext*/
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #region Users Identity tables

        //public DbSet<User> Users { get; set; }
        //public DbSet<UserToken> UserTokens { get; set; }
        //public DbSet<UserClaim> UserClaims { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<RoleClaim> RoleClaims { get; set; }
        //public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<CustomRole> CustomRoles { set; get; }
        public DbSet<CustomUser> CustomUsers { set; get; }

        #endregion

       
       
        public DbSet<Language> languages { set; get; }
        public DbSet<Order> Order { get; set; }
        public DbSet<IPAdressLog> IPAdressLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            MyDbContextSeed.SeedData(modelBuilder); // uncomment this to insert data
         
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new IPAdressLogConfiguration());

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // uncomment to start database logger 
            //var lf = new LoggerFactory();
            //lf.AddProvider(new MyLoggerProvider());
            //optionsBuilder.UseLoggerFactory(lf);

            optionsBuilder.UseLazyLoadingProxies();            
            base.OnConfiguring(optionsBuilder);
        }
    }
}
