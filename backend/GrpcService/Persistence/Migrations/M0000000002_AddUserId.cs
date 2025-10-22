using FluentMigrator;

namespace Persistence.Migrations;

[Migration(0000000002)]
public class M0000000002_AddUserId : Migration
{
    public override void Up()
    {
        Alter.Table("Users")
            .AddColumn("UserId").AsInt64();

        Create.Index("UX_Users_UserId")
            .OnTable("Users")
            .OnColumn("UserId")
            .Unique();
    }

    public override void Down()
    {
        Delete.Index("UX_Users_UserId").OnTable("Users");
        Delete.Column("UserId").FromTable("Users");
    }
}
