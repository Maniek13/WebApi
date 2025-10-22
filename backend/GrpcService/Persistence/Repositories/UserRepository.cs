using Abstarction.Repositories;
using Domain.Entities.StackOverFlow;
using NHibernate;

namespace Persistence.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly ISession _session;

    public UserRepository(ISession session)
    {
        _session = session;
    }

    public void Add(User user)
    {
        _session.Save(user);
    }
}
