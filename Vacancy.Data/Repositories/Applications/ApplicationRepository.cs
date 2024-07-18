using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacancy.Data.DbContexts;
using Vacancy.Data.IRepositories.Applicants;
using Vacancy.Data.IRepositories.Applications;
using Vacancy.Domain.Entities.Applications;

namespace Vacancy.Data.Repositories.Applications;

public class ApplicationRepository : Repository<Application>, IApplicationRepository
{
    private readonly AppDbContext _dbContext;
    public ApplicationRepository(AppDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public async Task<bool> DeleteApplicationsAsync(int[] ids)
    {
        var applications = _dbContext.Applications.Where(x => ids.Any(id => id == x.Id));
        _dbContext.RemoveRange(applications);

        return await _dbContext.SaveChangesAsync() > 0 ? true : false;
    }
}
