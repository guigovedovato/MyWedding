namespace MyWeddingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Text;
    public partial class FirstTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guest",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        UserName = c.String(maxLength: 30),
                        CreatedAt = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Login = c.String(maxLength: 15),
                        Name = c.String(maxLength: 30),
                        Password = c.String(maxLength: 40),
                        Profile = c.Int(nullable: false),
                        FirstPassword = c.String(maxLength: 8),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);

            var sql = new StringBuilder();
            sql.Append("INSERT INTO [MyWedding].[dbo].[User] VALUES ('ADM', 'Administrador', 'aa1bf4646de67fd9086cf6c79007026c', 0, NULL, NULL);");
            Sql(sql.ToString());

            CreateTable(
                "dbo.Log",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        InnerException = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LogType = c.Int(nullable: false),
                        UserName = c.String(maxLength: 30),
                        UserID = c.Int(nullable: false),
                        Controller = c.String(maxLength: 15),
                        Action = c.String(maxLength: 15),
                        Class = c.String(maxLength: 15),
                        Method = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guest", "UserID", "dbo.User");
            DropIndex("dbo.Log", new[] { "UserID" });
            DropIndex("dbo.Guest", new[] { "UserID" });
            DropTable("dbo.Log");
            DropTable("dbo.User");
            DropTable("dbo.Guest");
        }
    }
}
