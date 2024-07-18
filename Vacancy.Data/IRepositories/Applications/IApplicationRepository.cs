using Vacancy.Domain.Entities.Applications;

namespace Vacancy.Data.IRepositories.Applications;

public interface IApplicationRepository : IRepostory<Application>
{
    public Task<bool> DeleteApplicationsAsync(int[] ids);
}
