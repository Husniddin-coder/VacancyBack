using Vacancy.Domain.Entities.Authentications;

namespace Vacancy.Data.IRepositories.Authentication;

public interface IRolePermissionRepository : IRepostory<RolePermission>
{
    public bool AddRange(IEnumerable<RolePermission> rolePermissions);

    public bool RemoveRange(IEnumerable<RolePermission> rolePermissions);
}
