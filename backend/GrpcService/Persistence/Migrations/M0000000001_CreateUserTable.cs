using FluentMigrator;

namespace Persistence.Migrations;

[Migration(0000000001)]
public class M0000000001_CreateUserTable : Migration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsInt32().Identity().PrimaryKey()
            .WithColumn("AccountId").AsInt64()
            .WithColumn("DispalaName").AsString().Nullable()
            .WithColumn("CreatedAt").AsInt64().NotNullable();

        Create.Index("UX_Users_AccountId")
            .OnTable("Users")
            .OnColumn("AccountId")
            .Unique();
    }

    public override void Down()
    {
        Delete.Index("UX_Users_AccountId").OnTable("Users");
        Delete.Table("Users");
    }
}
