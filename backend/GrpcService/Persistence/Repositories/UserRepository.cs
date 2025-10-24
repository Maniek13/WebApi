using Abstarction.Repositories;
using Domain.Entities.StackOverFlow;
using NHibernate;

namespace Persistence.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly ISession _session;
    private readonly static AutoResetEvent _autoResetEvent = new AutoResetEvent(true);

    public UserRepository(ISession session)
    {
        _session = session;
    }

    public async Task AddOrUpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        using (var transaction = _session.BeginTransaction())
        {
            var tempUser = _session.Query<User>()
               .Where(u => u.UserId == user.UserId)
               .FirstOrDefault();

            if (tempUser == null)
                    _session.Save(tempUser);
            else
                tempUser.Update(user.DispalaName);

            await _session.FlushAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
    }

    public async Task AddOrUpdateAsync(List<User> users)
    {
        try
        {
            _autoResetEvent.WaitOne();
            using (var transaction = _session.BeginTransaction())
            {
                var userIds = users.Select(u => u.UserId).ToList();

                User[] toUpdate = Array.Empty<User>();
                if (userIds.Any())
                    toUpdate = _session.Query<User>()
                        .Where(u => userIds.Contains(u.UserId))
                        .ToArray();

                var toAdd = users.Where(el => !toUpdate.Any(u => el.UserId == u.UserId)).ToArray();

                if (toAdd != null)
                    for (int i = 0; i < toAdd.Length; ++i)
                        _session.Save(toAdd.ElementAt(i));

                if (toUpdate != null)
                    for (int i = 0; i < toUpdate.Length; ++i)
                        toUpdate[i].Update(users.First(el => el.UserId == toUpdate[i].UserId).DispalaName);

                await _session.FlushAsync();
                await transaction.CommitAsync();
            }

            _autoResetEvent.Set();
        }
        catch (Exception ex)
        {
            _autoResetEvent.Set();
            throw;
        }
       
    }
}
