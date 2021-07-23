using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asm.Areas.Identity.Data;
using Asm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Asm.Data
{
    public class DataContext : IdentityDbContext<DataUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            this.SeedRoles(builder);
            this.SeedUsers(builder);
            this.SeedUserToRole(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "fab4fac1-c546-41de-aebc-a14da6895711",
                    Name = "Admin",
                    /*                    ConcurrencyStamp = "1",*/
                    NormalizedName = "Admin"
                }
                );
        }
        private void SeedUsers(ModelBuilder builder)
        {
            DataUser user = new DataUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "admin@acb.com",
                NormalizedUserName = "admin@acb.com",
                Email = "admin@acb.com",
                LockoutEnabled = false,
                PhoneNumber = "0123456679",
                NormalizedEmail = "admin@abc.com",
                SecurityStamp = Guid.NewGuid().ToString(),

            };
            PasswordHasher<DataUser> pHash = new PasswordHasher<DataUser>();
            user.PasswordHash = pHash.HashPassword(user, "Admin*123");
            builder.Entity<DataUser>().HasData(user);
        }

        private void SeedUserToRole(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "fab4fac1-c546-41de-aebc-a14da6895711",
                    UserId = "b74ddd14-6340-4840-95c2-db12554843e5"
                }
                );
        }
    }
}
