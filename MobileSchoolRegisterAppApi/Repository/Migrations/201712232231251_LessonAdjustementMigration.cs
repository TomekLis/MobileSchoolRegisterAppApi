namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LessonAdjustementMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Lesson", new[] { "CourseId" });
            AlterColumn("dbo.Lesson", "CourseId", c => c.Int());
            CreateIndex("dbo.Lesson", "CourseId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Lesson", new[] { "CourseId" });
            AlterColumn("dbo.Lesson", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lesson", "CourseId");
        }
    }
}
