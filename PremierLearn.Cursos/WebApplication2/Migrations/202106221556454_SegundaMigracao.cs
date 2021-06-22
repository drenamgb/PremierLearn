namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SegundaMigracao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.livros", "Edicao", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.livros", "Nome", c => c.String(nullable: false, maxLength: 200, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.livros", "Nome", c => c.String(unicode: false));
            DropColumn("dbo.livros", "Edicao");
        }
    }
}
