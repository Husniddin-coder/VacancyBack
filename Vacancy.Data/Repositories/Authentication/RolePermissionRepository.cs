using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacancy.Data.DbContexts;
using Vacancy.Data.IRepositories.Authentication;
using Vacancy.Domain.Entities.Authentications;

namespace Vacancy.Data.Repositories.Authentication
{
    public class RolePermissionRepository : Repository<RolePermission>, IRolePermissionRepository
    {
        private readonly AppDbContext _appDbContext;
        public RolePermissionRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public bool AddRange(IEnumerable<RolePermission> rolePermissions)
        {
            _appDbContext.AddRange(rolePermissions);

            return _appDbContext.SaveChanges() > 0 ? true : false;
        }

        public bool RemoveRange(IEnumerable<RolePermission> rolePermissions)
        {
            _appDbContext.RemoveRange(rolePermissions);

            return _appDbContext.SaveChanges() > 0 ? true : false;
        }
    }
}
