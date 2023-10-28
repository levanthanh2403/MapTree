using map.backend.shared.Interfaces;
using map.backend.shared.Interfaces.UnitOfWork;
using map.backend.shared.Persistence;
using map.backend.shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using map.backend.shared.Interfaces.Auth;
using map.backend.shared.Repositories.Auth;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using map.backend.shared.Handler;
using map.backend.shared.Interfaces.Map;
using map.backend.shared.Repositories.Map;
using map.backend.shared.Interfaces.Report;
using map.backend.shared.Repositories.Report;

namespace map.backend.shared
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connString, o => o.UseNetTopologySuite()).UseLowerCaseNamingConvention();
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.Configure<ApiBehaviorOptions>(x => x.InvalidModelStateResponseFactory = ctx => new ValidationProblemDetailsResult());

            services.AddControllers()
              .AddJsonOptions(options =>
              {
                  options.JsonSerializerOptions.Converters.Add(
                      new NetTopologySuite.IO.Converters.GeoJsonConverterFactory());
              });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
            services.AddScoped<IUnitOfWork<AppDbContext>, UnitOfWork<AppDbContext>>();

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();


            return services;
        }
    }
}
