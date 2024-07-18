using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Vacancy.Api.Attributes;
using Vacancy.Api.MIddlewares;
using Vacancy.Data.DbContexts;
using Vacancy.Data.IRepositories.Applicants;
using Vacancy.Data.IRepositories.Applications;
using Vacancy.Data.IRepositories.Assetss;
using Vacancy.Data.IRepositories.Authentication;
using Vacancy.Data.IRepositories.Vacancies;
using Vacancy.Data.Repositories.Applicants;
using Vacancy.Data.Repositories.Applications;
using Vacancy.Data.Repositories.Assetss;
using Vacancy.Data.Repositories.Authentication;
using Vacancy.Data.Repositories.Vacancies;
using Vacancy.Domain.Entities.Authentications;
using Vacancy.Domain.Enums.Users;
using Vacancy.Service.DTOs.AuthDtos.JWTOptions;
using Vacancy.Service.Interfaces.Applicatoins;
using Vacancy.Service.Interfaces.Assetss;
using Vacancy.Service.Interfaces.Authentication;
using Vacancy.Service.Interfaces.Authentication.Tokens;
using Vacancy.Service.Interfaces.Vacancies;
using Vacancy.Service.Mappers;
using Vacancy.Service.Services.Applications;
using Vacancy.Service.Services.Assetss;
using Vacancy.Service.Services.Authentication;
using Vacancy.Service.Services.Authentication.Tokens;
using Vacancy.Service.Services.Vacancies;

namespace Vacancy.Api.Extensions;

public static class RegisterServices
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        //DbContext
        services.AddDbContextPool<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            options.UseLazyLoadingProxies();
        });

        //ModelState Validation
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidateModelAttribute>();
        });

        //JWTOptons
        services.Configure<JWTOption>(configuration.GetSection("JWT"));

        //Mapper
        services.AddAutoMapper(typeof(MappingProfile));

        //Swagger configurations
        services.AddSwaggerGen(options =>
        {
            //options
            //.SwaggerDoc("Admin", new OpenApiInfo { Title = AppDomain.CurrentDomain.FriendlyName});

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Bearer Authentication with JWT Token",
                Type = SecuritySchemeType.Http
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
              new OpenApiSecurityScheme
              {
                Reference = new OpenApiReference
                  {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                  }
               },
                new List<string>()
            }
        });
        });

        //Cors
        services.AddCors(options =>
        {
            options.AddPolicy(
                name: "AllowCors",
                policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
        });

        //Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["JWT:Audiance"],
                    ValidIssuer = configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = (context) =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("IsTokenExpired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization();

        //Repositories
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();

        services.AddScoped<IAssetsRepository,AssetsRepository>();
        services.AddScoped<IVakancyRepository,VakancyRepository>();
        services.AddScoped<IApplicantRepository,ApplicantRepository>();
        services.AddScoped<IApplicationRepository,ApplicationRepository>();

        //Services
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAssetsService, AssetsService>();
        services.AddScoped<IJWTokenService,JWTokenService>();
        services.AddScoped<IVakancyService, VakancyService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IApplicationService,ApplicationService>();


        return services;
    }

    public static void ConfigureDefault(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        scope.Dispose();

        //Syncronize Permissions
        app.SyncronizePermissions();
    }

    //Saving and Removing Permissions from Database automatically when there is change in permission enum
    private static void SyncronizePermissions(this WebApplication app)
    {
        try
        {
            //Logging should be done
            using var scope = app.Services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

            var permissionCodes = typeof(UserPermission).GetEnumValues().Cast<object>();

            var enumCodes = permissionCodes as object[] ?? permissionCodes.ToArray();

            foreach (var permissionCode in enumCodes)
            {
                var storedCode = dbContext.Permissions.FirstOrDefault(x => x.Code == (int)permissionCode);
                if (storedCode is not null) continue;

                dbContext.Permissions.Add(new Permission
                {
                    Code = (int)permissionCode,
                    Name = permissionCode.ToString() ?? string.Empty
                });
                continue;
            }
            dbContext.SaveChanges();

            var codes = enumCodes.Cast<int>();

            var romovedFromEnumPermissions = dbContext.Permissions
                .Where(x => codes.All(a => a != x.Code))
                .ToList();

            if (!romovedFromEnumPermissions.IsNullOrEmpty())
            {
                dbContext.Permissions.RemoveRange(romovedFromEnumPermissions);
                dbContext.SaveChanges();
            }

            //Logging should be done telling about successfully completion of syncronize Permissions

        }
        catch (Exception ex)
        {

        }
    }
}
