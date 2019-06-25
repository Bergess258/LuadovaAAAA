namespace BdOfResume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Somech1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TypeAcc", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TypeAcc");
        }
    }
}
