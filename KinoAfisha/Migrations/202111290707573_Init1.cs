namespace KinoAfisha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cinemas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CinemaPlace = c.String(nullable: false),
                        NumberOfBilets = c.Int(nullable: false),
                        QRcode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kinoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NextArrivalDate = c.DateTime(nullable: false),
                        KinoTime = c.DateTime(nullable: false),
                        DescriptionId = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Descriptions", t => t.DescriptionId, cascadeDelete: true)
                .Index(t => t.DescriptionId);
            
            CreateTable(
                "dbo.Descriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DescriptionCinemaCorporation = c.String(nullable: false),
                        DescriptionAllFilms = c.String(nullable: false),
                        DescriptionAllActors = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameFilm = c.String(nullable: false),
                        FilmYears = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FilmCovers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Size = c.Long(nullable: false),
                        Path = c.String(),
                        Guid = c.Guid(nullable: false),
                        Data = c.Binary(nullable: false),
                        ContentType = c.String(maxLength: 100),
                        DateChanged = c.DateTime(),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Films", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Formats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KinoCinemas",
                c => new
                    {
                        Kino_Id = c.Int(nullable: false),
                        Cinema_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Kino_Id, t.Cinema_Id })
                .ForeignKey("dbo.Kinoes", t => t.Kino_Id, cascadeDelete: true)
                .ForeignKey("dbo.Cinemas", t => t.Cinema_Id, cascadeDelete: true)
                .Index(t => t.Kino_Id)
                .Index(t => t.Cinema_Id);
            
            CreateTable(
                "dbo.FormatFilms",
                c => new
                    {
                        Format_Id = c.Int(nullable: false),
                        Film_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Format_Id, t.Film_Id })
                .ForeignKey("dbo.Formats", t => t.Format_Id, cascadeDelete: true)
                .ForeignKey("dbo.Films", t => t.Film_Id, cascadeDelete: true)
                .Index(t => t.Format_Id)
                .Index(t => t.Film_Id);
            
            CreateTable(
                "dbo.FilmKinoes",
                c => new
                    {
                        Film_Id = c.Int(nullable: false),
                        Kino_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Film_Id, t.Kino_Id })
                .ForeignKey("dbo.Films", t => t.Film_Id, cascadeDelete: true)
                .ForeignKey("dbo.Kinoes", t => t.Kino_Id, cascadeDelete: true)
                .Index(t => t.Film_Id)
                .Index(t => t.Kino_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilmKinoes", "Kino_Id", "dbo.Kinoes");
            DropForeignKey("dbo.FilmKinoes", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.FormatFilms", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.FormatFilms", "Format_Id", "dbo.Formats");
            DropForeignKey("dbo.FilmCovers", "Id", "dbo.Films");
            DropForeignKey("dbo.Kinoes", "DescriptionId", "dbo.Descriptions");
            DropForeignKey("dbo.KinoCinemas", "Cinema_Id", "dbo.Cinemas");
            DropForeignKey("dbo.KinoCinemas", "Kino_Id", "dbo.Kinoes");
            DropIndex("dbo.FilmKinoes", new[] { "Kino_Id" });
            DropIndex("dbo.FilmKinoes", new[] { "Film_Id" });
            DropIndex("dbo.FormatFilms", new[] { "Film_Id" });
            DropIndex("dbo.FormatFilms", new[] { "Format_Id" });
            DropIndex("dbo.KinoCinemas", new[] { "Cinema_Id" });
            DropIndex("dbo.KinoCinemas", new[] { "Kino_Id" });
            DropIndex("dbo.FilmCovers", new[] { "Id" });
            DropIndex("dbo.Kinoes", new[] { "DescriptionId" });
            DropTable("dbo.FilmKinoes");
            DropTable("dbo.FormatFilms");
            DropTable("dbo.KinoCinemas");
            DropTable("dbo.Formats");
            DropTable("dbo.FilmCovers");
            DropTable("dbo.Films");
            DropTable("dbo.Descriptions");
            DropTable("dbo.Kinoes");
            DropTable("dbo.Cinemas");
        }
    }
}
