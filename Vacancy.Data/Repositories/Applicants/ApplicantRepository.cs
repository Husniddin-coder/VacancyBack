using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacancy.Data.DbContexts;
using Vacancy.Data.IRepositories.Applicants;
using Vacancy.Domain.Entities.Applicants;

namespace Vacancy.Data.Repositories.Applicants
{
    public class ApplicantRepository : Repository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(AppDbContext context) : base(context) 
        {}
    }
}
