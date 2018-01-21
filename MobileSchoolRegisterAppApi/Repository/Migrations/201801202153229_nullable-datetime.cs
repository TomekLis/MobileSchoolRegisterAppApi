namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullabledatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lesson", "Date", c => c.DateTime());
            AlterColumn("dbo.DaySchedule", "StartTime", c => c.DateTime());
            AlterColumn("dbo.DaySchedule", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DaySchedule", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DaySchedule", "StartTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Lesson", "Date", c => c.DateTime(nullable: false));
        }
    }
}
