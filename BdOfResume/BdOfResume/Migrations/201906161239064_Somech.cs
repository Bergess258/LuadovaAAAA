namespace BdOfResume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Somech : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IdEmp", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IdEmp");
        }
    }
}
