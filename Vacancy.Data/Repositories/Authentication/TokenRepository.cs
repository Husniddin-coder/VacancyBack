using Vacancy.Data.DbContexts;
using Vacancy.Data.IRepositories.Authentication;
using Vacancy.Domain.Entities.Authentications;

namespace Vacancy.Data.Repositories.Authentication
{
    public class TokenRepository : Repository<TokenModel>, ITokenRepository
    {
        public TokenRepository(AppDbContext context) : base(context)
        {}
    }
}
