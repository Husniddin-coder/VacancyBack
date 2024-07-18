using Vacancy.Data.DbContexts;
using Vacancy.Data.IRepositories.Vacancies;
using Vacancy.Domain.Entities.Vacancies;

namespace Vacancy.Data.Repositories.Vacancies;

public class VakancyRepository : Repository<Vakancy>, IVakancyRepository
{
    public VakancyRepository(AppDbContext context) : base(context)
    { }
}
