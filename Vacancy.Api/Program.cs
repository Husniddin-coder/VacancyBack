
using Vacancy.Api.Extensions;
using Vacancy.Api.MIddlewares;
using Vacancy.Service.Helpers;

namespace Vacancy.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            WebHostEnvironmentHelper.WebRootPath = builder.Environment.WebRootPath;

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCustomServices(builder.Configuration);
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseCors("AllowCors");

            app.UseStaticFiles();

            app.UseAuthorization();

            app.ConfigureDefault();

            app.MapControllers();

            app.Run();
        }
    }
}
