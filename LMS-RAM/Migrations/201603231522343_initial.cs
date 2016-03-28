namespace LMS_RAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        ScheduleItemId = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        Grade = c.Int(),
                        Comment = c.String(maxLength: 200),
                        FileName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScheduleItems", t => t.ScheduleItemId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.ScheduleItemId);
            
            CreateTable(
                "dbo.ScheduleItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Mandatory = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(),
                        Name = c.String(maxLength: 50),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Points = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SSN = c.String(nullable: false, maxLength: 13),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SSN = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Grade = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.StudentShareds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Description = c.String(maxLength: 50),
                        FileName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.TeacherShareds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        Description = c.String(maxLength: 50),
                        FileName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.TeacherId);
            
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
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeacherShareds", "TeacherId", "dbo.Students");
            DropForeignKey("dbo.TeacherShareds", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.StudentShareds", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentShareds", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.StudentCourses", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Assignments", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Assignments", "ScheduleItemId", "dbo.ScheduleItems");
            DropForeignKey("dbo.ScheduleItems", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TeacherShareds", new[] { "TeacherId" });
            DropIndex("dbo.TeacherShareds", new[] { "CourseId" });
            DropIndex("dbo.StudentShareds", new[] { "StudentId" });
            DropIndex("dbo.StudentShareds", new[] { "CourseId" });
            DropIndex("dbo.StudentCourses", new[] { "StudentId" });
            DropIndex("dbo.StudentCourses", new[] { "CourseId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Courses", new[] { "TeacherId" });
            DropIndex("dbo.ScheduleItems", new[] { "CourseId" });
            DropIndex("dbo.Assignments", new[] { "ScheduleItemId" });
            DropIndex("dbo.Assignments", new[] { "StudentId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TeacherShareds");
            DropTable("dbo.StudentShareds");
            DropTable("dbo.StudentCourses");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Students");
            DropTable("dbo.Teachers");
            DropTable("dbo.Courses");
            DropTable("dbo.ScheduleItems");
            DropTable("dbo.Assignments");
        }
    }
}
