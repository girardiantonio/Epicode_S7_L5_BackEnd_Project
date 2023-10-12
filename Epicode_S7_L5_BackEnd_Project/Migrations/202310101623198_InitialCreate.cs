namespace Epicode_S7_L5_BackEnd_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DettaglioOrdine",
                c => new
                    {
                        IdDettaglioOrdine = c.Int(nullable: false, identity: true),
                        IdOrdine = c.Int(),
                        IdProdotto = c.Int(),
                        Quantita = c.Int(),
                    })
                .PrimaryKey(t => t.IdDettaglioOrdine)
                .ForeignKey("dbo.Ordine", t => t.IdOrdine)
                .ForeignKey("dbo.Prodotto", t => t.IdProdotto)
                .Index(t => t.IdOrdine)
                .Index(t => t.IdProdotto);
            
            CreateTable(
                "dbo.Ordine",
                c => new
                    {
                        IdOrdine = c.Int(nullable: false, identity: true),
                        IdUtente = c.Int(),
                        DataOrdine = c.DateTime(),
                        Importo = c.Decimal(precision: 18, scale: 2),
                        IndirizzoConsegna = c.String(maxLength: 255, unicode: false),
                        NoteSpeciali = c.String(maxLength: 255, unicode: false),
                        Evaso = c.Boolean(),
                    })
                .PrimaryKey(t => t.IdOrdine)
                .ForeignKey("dbo.Utente", t => t.IdUtente)
                .Index(t => t.IdUtente);
            
            CreateTable(
                "dbo.Utente",
                c => new
                    {
                        IdUtente = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 255, unicode: false),
                        Cognome = c.String(maxLength: 255, unicode: false),
                        Provincia = c.String(maxLength: 255, unicode: false),
                        Citta = c.String(maxLength: 255, unicode: false),
                        Indirizzo = c.String(maxLength: 255, unicode: false),
                        Email = c.String(maxLength: 255, unicode: false),
                        Password = c.String(maxLength: 255, unicode: false),
                        IdRuolo = c.Int(),
                    })
                .PrimaryKey(t => t.IdUtente)
                .ForeignKey("dbo.Ruolo", t => t.IdRuolo)
                .Index(t => t.IdRuolo);
            
            CreateTable(
                "dbo.Ruolo",
                c => new
                    {
                        IdRuolo = c.Int(nullable: false, identity: true),
                        Role = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.IdRuolo);
            
            CreateTable(
                "dbo.Prodotto",
                c => new
                    {
                        IdProdotto = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 255, unicode: false),
                        FotoUrl = c.String(maxLength: 255, unicode: false),
                        Prezzo = c.Decimal(precision: 18, scale: 2),
                        TempoConsegna = c.Int(),
                        Ingredienti = c.String(maxLength: 255, unicode: false),
                })
                .PrimaryKey(t => t.IdProdotto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DettaglioOrdine", "IdProdotto", "dbo.Prodotto");
            DropForeignKey("dbo.Utente", "IdRuolo", "dbo.Ruolo");
            DropForeignKey("dbo.Ordine", "IdUtente", "dbo.Utente");
            DropForeignKey("dbo.DettaglioOrdine", "IdOrdine", "dbo.Ordine");
            DropIndex("dbo.Utente", new[] { "IdRuolo" });
            DropIndex("dbo.Ordine", new[] { "IdUtente" });
            DropIndex("dbo.DettaglioOrdine", new[] { "IdProdotto" });
            DropIndex("dbo.DettaglioOrdine", new[] { "IdOrdine" });
            DropTable("dbo.Prodotto");
            DropTable("dbo.Ruolo");
            DropTable("dbo.Utente");
            DropTable("dbo.Ordine");
            DropTable("dbo.DettaglioOrdine");
        }
    }
}
