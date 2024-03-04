using MediatR;
using VM.API.Pipelines;
using VM.Common.Configurations;
using VM.Common.Models;
using VM.Common.Services;
using VM.DataAccess.Repositories;

namespace VM.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.RegisterConfiguration(configuration);
            services.RegisterServices();
            services.RegisterRepositories();
            services.RegisterMediatRPipeline();
        }

        public static void ConfigureJobs(this IApplicationBuilder app)
        {
        }

        private static void RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WebAppConfiguration>(configuration.GetSection(WebAppConfiguration.SettingName));
            services.Configure<JWTConfiguration>(configuration.GetSection(JWTConfiguration.SettingName));
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
            services.AddScoped<IFeesHeadRepository, FeesHeadRepository>();
            services.AddScoped<IFeesStructureRepository, FeesStructureRepository>();
        }

        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<ICryptoHashService, CryptoHashService>();
        }

        private static void RegisterMediatRPipeline(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehaviour<,>));
        }
    }
}
