using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Linq;
using System.Reflection;

namespace Api.Infrastructure
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            AppDomain
                .CurrentDomain
                .GetAssemblies()
                .ToList()
                .ForEach(x => builder.ApplyConfigurationsFromAssembly(x));
        }

        public class AppContextDesignFactory : IDesignTimeDbContextFactory<AppContext>
        {
            public AppContext CreateDbContext(string[] args)
            {

                var optionsBuilder = new DbContextOptionsBuilder<AppContext>()
                                .UseSqlServer("Server=sqldata;Database=UsersDb;user id=sa;password=Passw*rd1");

                return new AppContext(optionsBuilder.Options);
            }
        }
    }
}
