namespace MyWeddingSystem.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Text;

    public partial class Tables_MYSQL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guest",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        UserName = c.String(maxLength: 30, storeType: "nvarchar"),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
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
                        Login = c.String(maxLength: 15, storeType: "nvarchar"),
                        Name = c.String(maxLength: 30, storeType: "nvarchar"),
                        Password = c.String(maxLength: 40, storeType: "nvarchar"),
                        Profile = c.Int(nullable: false),
                        FirstPassword = c.String(maxLength: 8, storeType: "nvarchar"),
                        UpdatedAt = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.ID);

            var sql = new StringBuilder();
            sql.Append("INSERT INTO mywedding.User VALUES (1, 'ADM', 'Administrador', 'aa1bf4646de67fd9086cf6c79007026c', 0, NULL, NULL);");
            Sql(sql.ToString());

            CreateTable(
                "dbo.Log",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(unicode: false),
                        StackTrace = c.String(unicode: false),
                        InnerException = c.String(unicode: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        LogType = c.Int(nullable: false),
                        UserName = c.String(maxLength: 30, storeType: "nvarchar"),
                        UserID = c.Int(nullable: false),
                        Controller = c.String(maxLength: 15, storeType: "nvarchar"),
                        Action = c.String(maxLength: 15, storeType: "nvarchar"),
                        Class = c.String(maxLength: 15, storeType: "nvarchar"),
                        Method = c.String(maxLength: 15, storeType: "nvarchar"),
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
