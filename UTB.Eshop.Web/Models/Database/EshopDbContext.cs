using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTB.Eshop.Web.Models.Entities;
using UTB.Eshop.Web.Models.Identity;

namespace UTB.Eshop.Web.Models.Database
{
    public class EshopDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<CarouselItem> CarouselItems { get; set; }

        public EshopDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().Replace("AspNet", String.Empty));
            }

            DatabaseInit dbInit = new DatabaseInit();
            builder.Entity<CarouselItem>().HasData(dbInit.CreateCarouselItems());
            builder.Entity<Role>().HasData(dbInit.CreateRoles());

            (User admin, List<IdentityUserRole<int>> adminRole) = dbInit.CreateAdminWithRoles();
            (User manager, List<IdentityUserRole<int>> managerRole) = dbInit.CreateManagerWithRoles();

            builder.Entity<User>().HasData(admin, manager);
            builder.Entity<IdentityUserRole<int>>().HasData(adminRole);
            builder.Entity<IdentityUserRole<int>>().HasData(managerRole);
        }
    }
}
