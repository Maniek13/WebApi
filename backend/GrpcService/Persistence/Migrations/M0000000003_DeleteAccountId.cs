using FluentMigrator;

namespace Persistence.Migrations;

[Migration(0000000003)]
public class M0000000003_DeleteAccountId : Migration
{
    public override void Up()
    {
        Delete.Index("UX_Users_AccountId").OnTable("Users");
        Delete.Column("AccountId").FromTable("Users");
    }

    public override void Down()
    {
        Alter.Table("Users")
            .AddColumn("AccountId");

        Create.Index("UX_Users_AccountId")
          .OnTable("Users")
          .OnColumn("AccountId")
          .Unique();
    }
}
