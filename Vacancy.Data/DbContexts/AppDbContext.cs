using Microsoft.EntityFrameworkCore;
using Vacancy.Domain.Entities.Applicants;
using Vacancy.Domain.Entities.Applications;
using Vacancy.Domain.Entities.Assetss;
using Vacancy.Domain.Entities.Authentications;
using Vacancy.Domain.Entities.Vacancies;

namespace Vacancy.Data.DbContexts;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public DbSet<User> Users { get;}
    public DbSet<Role> Roles { get; set; }
    public DbSet<Assets> Assets { get; set; }
    public DbSet<Vakancy> Vakancies { get; set; }
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<TokenModel> TokenModels { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
}
