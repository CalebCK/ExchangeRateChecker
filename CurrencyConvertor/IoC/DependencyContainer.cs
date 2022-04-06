using CurrencyConvertor.V1.Repositories;
using CurrencyConvertor.V1.Repositories.IRepositories;
using CurrencyConvertor.V1.Repository;
using CurrencyConvertor.V1.Repository.IRepository;
using CurrencyConvertor.V1.Services;
using CurrencyConvertor.V1.Services.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConvertor.IoC
{
    /// <summary>
    /// An extension for Dependency Injections
    /// </summary>
    public static class DependencyContainer
    {
        public static void RegisterConnections<T>(this IServiceCollection services, string connection) where T : DbContext
        {
            services.AddDbContext<T>(options => options.UseNpgsql(connection));

        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IExchangeRateRequestRepository, ExchangeRateRequestRepository>();
        }
    }
}
