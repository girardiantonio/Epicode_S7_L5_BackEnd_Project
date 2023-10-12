namespace Epicode_S7_L5_BackEnd_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prodotto", "Ingredienti", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prodotto", "Ingredienti", c => c.String());
        }
    }
}
