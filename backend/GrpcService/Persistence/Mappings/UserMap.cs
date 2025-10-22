using Domain.Entities.StackOverFlow;
using FluentNHibernate.Mapping;

namespace Persistence.Mappings;

internal class UserMap : ClassMap<User>
{
    public UserMap()
    {
        Table("Users");

        Id(x => x.Id)
            .Column("Id")
            .GeneratedBy.Identity();
        Map(x => x.UserId)
            .Column("UserId")
            .Not.Nullable()
            .Unique()
            .Index("UX_Users_UserId");
        Map(x => x.DispalaName)
            .Column("DispalaName")
            .Nullable();
        Map(x => x.CreatedAt)
            .Column("CreatedAt")
            .Not.Nullable();
    }
}
