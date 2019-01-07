namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user07 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "UId");
            AddColumn("dbo.AspNetUsers", "UserId", c => c.Int(nullable: false, identity: true));
            


        }
        
        public override void Down()
        {
        }
    }
}
