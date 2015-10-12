namespace WebApplication1.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using System.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebApplication1.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var AdminUser = ConfigurationManager.AppSettings["AdminUser"];
            var Passwd = ConfigurationManager.AppSettings["AdminPassword"];
            if (!context.Users.Any(u => u.Email == AdminUser))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = AdminUser,
                    Email = AdminUser,
                    FirstName = "Allan",
                    LastName = "Clark",
                    DisplayName = "aclark"
                }, Passwd);
            }

            var userId = userManager.FindByEmail(AdminUser).Id;
            userManager.AddToRole(userId, "Admin");

            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            if(!context.Users.Any(u => u.Email == "moderator@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "moderator@coderfoundry.com",
                    Email = "moderator@coderfoundry.com",
                    FirstName = "Moderator",
                    LastName = "Moderator",
                    DisplayName = "Mod"
                }, "CoderFoundry");
            }
        }
    }
}
