using Vacancy.Data.DbContexts;
using Vacancy.Data.IRepositories.Authentication;
using Vacancy.Domain.Entities.Authentications;

namespace Vacancy.Data.Repositories.Authentication;

public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    public PermissionRepository(AppDbContext context) : base(context)
    {
    }
}
