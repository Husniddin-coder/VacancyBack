using Vacancy.Data.DbContexts;
using Vacancy.Data.IRepositories.Assetss;
using Vacancy.Domain.Entities.Assetss;

namespace Vacancy.Data.Repositories.Assetss;

public class AssetsRepository : Repository<Assets>, IAssetsRepository
{
    public AssetsRepository(AppDbContext context) : base(context)
    { }
}
