using Application;
using Application.Models;
using Core.EF.Repositories;
using Core.Repositories;
using Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure
{
    public static class Config
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services
                .AddScoped<IAggregateRepository<User>, AggregateRepository<User>>()
                .AddScoped<IModelRepository<UserDetail>, ModelRepository<UserDetail>>()
                .AddScoped<IModelRepository<AddressHistory>, ModelRepository<AddressHistory>>()
                .AddSqlDbCOntext()
                .AddApplicationServices();

            return services;
        }

        private static IServiceCollection AddSqlDbCOntext(this IServiceCollection services)
        {
            services.AddDbContext<DbContext, AppContext>(
                options => options
                            .UseLazyLoadingProxies()
                            .UseSqlServer("name=ConnectionStrings:DefaultConnection"));

            return services;
        }
    }
}
