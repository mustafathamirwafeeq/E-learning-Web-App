namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user02 : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.AspNetUsers", "UserId");
            //AddColumn("dbo.AspNetUsers", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.AspNetUsers", "UserId");
        }
    }
}
