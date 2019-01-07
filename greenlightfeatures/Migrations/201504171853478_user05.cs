namespace ELearning.Migrations
{
    using ELearning.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user05 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UId", c => c.Int(nullable: false, identity: true));
            
        }
        
        public override void Down()
        {
            
        }
    }
}
