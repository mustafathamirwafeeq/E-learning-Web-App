namespace ELearning.Migrations
{
    using ELearning.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user04 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.AspNetUsers", "UId", c => c.Int(nullable: false, identity: true));

            //IdentityResult IdUserResult;
            //IdentityResult IdRoleResult;
            //Models.ApplicationDbContext context = new ApplicationDbContext();

            //var roleStore = new RoleStore<IdentityRole>(context);
            //var roleMgr = new RoleManager<IdentityRole>(roleStore);
            //if (!roleMgr.RoleExists("Admin"))
            //{
            //    IdRoleResult = roleMgr.Create(new IdentityRole { Name = "Admin" });
            //}
            //if (!roleMgr.RoleExists("Moderator"))
            //{
            //    IdRoleResult = roleMgr.Create(new IdentityRole { Name = "Moderator" });
            //}
            //if (!roleMgr.RoleExists("Student"))
            //{
            //    IdRoleResult = roleMgr.Create(new IdentityRole { Name = "Student" });
            //}
            //if (!roleMgr.RoleExists("Visitor"))
            //{
            //    IdRoleResult = roleMgr.Create(new IdentityRole { Name = "Visitor" });
            //}
            
            //var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //var appUser = new ApplicationUser
            //{
            //    UserName = "admin",
            //    DateOfBirth = DateTime.Now,
            //    Email = "admin@abc.com",
            //    FullName = "Administrator",
            //    PhoneNumber = "+92300 4218381"
            //};
            //IdUserResult = userMgr.Create(appUser, "Admin*123");

            //if (!userMgr.IsInRole(userMgr.FindByName("admin").Id, "Admin"))
            //{
            //    IdUserResult = userMgr.AddToRole(userMgr.FindByName("admin").Id, "Admin");
            //}
        }
        
        public override void Down()
        {
        }
    }
}
