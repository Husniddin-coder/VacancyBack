using Vacancy.Data.DbContexts;
using Vacancy.Data.IRepositories.Authentication;
using Vacancy.Domain.Entities.Authentications;

namespace Vacancy.Data.Repositories.Authentication;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    { }
}
