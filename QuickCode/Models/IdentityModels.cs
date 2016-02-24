using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuickCode.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // for temporary security only
        public string RoleName { get; set; }   
        public int? AccessID { get; set; }
        public virtual ICollection<Production> Production { get; set; }
        // downtime added 14/12/2015
        public virtual ICollection<Downtime> Downtime { get; set; }
        // all the fancy foreign keys
        public virtual ICollection<UserAccessTypes> UserAccess { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("WDIBPW", throwIfV1Schema: false)
        // CHANGE THIS FOR LOCAL IRWINS - WDIBPW
        // DefaultConnection for local testing - dev laptops
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // Adding modified code
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<DowntimeType> DowntimeTypes { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<Downtime> Downtime { get; set; }
        public DbSet<AccessTypes> AccessTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<AccessTypes>().ToTable("AccessTypes").Property(p => p.AccessID).HasColumnName("AccessID");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim").Property(p => p.Id).HasColumnName("UserClaimId");
            modelBuilder.Entity<IdentityRole>().ToTable("Role").Property(p => p.Id).HasColumnName("RoleId");

        }

        public System.Data.Entity.DbSet<QuickCode.Models.UserAccessTypes> UserAccessTypes { get; set; }

        public System.Data.Entity.DbSet<QuickCode.Models.FiscalWeek> FiscalWeeks { get; set; }
    }
}