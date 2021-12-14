namespace KinoAfisha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Kinoes", "DescriptionId", "dbo.Descriptions");
            DropIndex("dbo.Kinoes", new[] { "DescriptionId" });
            RenameColumn(table: "dbo.Kinoes", name: "DescriptionId", newName: "Description_Id");
            AddColumn("dbo.Films", "FilmDescription", c => c.String(nullable: false));
            AddColumn("dbo.Films", "FilmAllActors", c => c.String(nullable: false));
            AddColumn("dbo.Films", "FilmDop", c => c.String());
            AlterColumn("dbo.Kinoes", "Description_Id", c => c.Int());
            CreateIndex("dbo.Kinoes", "Description_Id");
            AddForeignKey("dbo.Kinoes", "Description_Id", "dbo.Descriptions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kinoes", "Description_Id", "dbo.Descriptions");
            DropIndex("dbo.Kinoes", new[] { "Description_Id" });
            AlterColumn("dbo.Kinoes", "Description_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Films", "FilmDop");
            DropColumn("dbo.Films", "FilmAllActors");
            DropColumn("dbo.Films", "FilmDescription");
            RenameColumn(table: "dbo.Kinoes", name: "Description_Id", newName: "DescriptionId");
            CreateIndex("dbo.Kinoes", "DescriptionId");
            AddForeignKey("dbo.Kinoes", "DescriptionId", "dbo.Descriptions", "Id", cascadeDelete: true);
        }
    }
}
