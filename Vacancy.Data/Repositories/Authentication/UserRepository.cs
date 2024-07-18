using Vacancy.Data.DbContexts;
using Vacancy.Data.IRepositories.Authentication;
using Vacancy.Domain.Entities.Authentications;

namespace Vacancy.Data.Repositories.Authentication;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {}
}
