namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigrationCorrect : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentActivity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LessonId = c.Int(nullable: false),
                        StudentId = c.String(maxLength: 128),
                        WasPresent = c.Boolean(),
                        MarkValue = c.Int(),
                        Importance = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lesson", t => t.LessonId)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .Index(t => t.LessonId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Lesson",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Room = c.String(),
                        TeacherId = c.String(maxLength: 128),
                        StudentsGroupId = c.Int(),
                        StudentGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentGroup", t => t.StudentGroup_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TeacherId)
                .Index(t => t.TeacherId)
                .Index(t => t.StudentGroup_Id);
            
            CreateTable(
                "dbo.DaySchedule",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.StudentGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Grade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(),
                        LastName = c.String(),
                        StudentsGroupId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        StudentGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentGroup", t => t.StudentGroup_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.StudentGroup_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.StudentActivity", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentActivity", "LessonId", "dbo.Lesson");
            DropForeignKey("dbo.Lesson", "CourseId", "dbo.Course");
            DropForeignKey("dbo.Course", "TeacherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Course", "StudentGroup_Id", "dbo.StudentGroup");
            DropForeignKey("dbo.AspNetUsers", "StudentGroup_Id", "dbo.StudentGroup");
            DropForeignKey("dbo.DaySchedule", "CourseId", "dbo.Course");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "StudentGroup_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.DaySchedule", new[] { "CourseId" });
            DropIndex("dbo.Course", new[] { "StudentGroup_Id" });
            DropIndex("dbo.Course", new[] { "TeacherId" });
            DropIndex("dbo.Lesson", new[] { "CourseId" });
            DropIndex("dbo.StudentActivity", new[] { "StudentId" });
            DropIndex("dbo.StudentActivity", new[] { "LessonId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.StudentGroup");
            DropTable("dbo.DaySchedule");
            DropTable("dbo.Course");
            DropTable("dbo.Lesson");
            DropTable("dbo.StudentActivity");
        }
    }
}
