namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelAdjustement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentActivity", "Student_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.StudentActivity", "Student_Id1", c => c.String(maxLength: 128));
            CreateIndex("dbo.StudentActivity", "Student_Id");
            CreateIndex("dbo.StudentActivity", "Student_Id1");
            AddForeignKey("dbo.StudentActivity", "Student_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.StudentActivity", "Student_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentActivity", "Student_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentActivity", "Student_Id", "dbo.AspNetUsers");
            DropIndex("dbo.StudentActivity", new[] { "Student_Id1" });
            DropIndex("dbo.StudentActivity", new[] { "Student_Id" });
            DropColumn("dbo.StudentActivity", "Student_Id1");
            DropColumn("dbo.StudentActivity", "Student_Id");
        }
    }
}
