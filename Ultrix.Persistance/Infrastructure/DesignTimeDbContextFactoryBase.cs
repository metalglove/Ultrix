using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace Ultrix.Persistance.Infrastructure
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
        protected abstract string ConnectionStringName { get; }
        protected string ConnectionString { get; private set; }
        protected DbContextOptionsBuilder<TContext> OptionsBuilder { get; private set; }

        protected DesignTimeDbContextFactoryBase(bool precompileConnectionString = false)
        {
            if (!precompileConnectionString) return;
            SetConnectionString(BuildConfiguration());
            SetDbContextOptionsBuilder(ConnectionString);
        }

        private void SetConnectionString(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString(ConnectionStringName);
        }
        private static IConfigurationRoot BuildConfiguration()
        {
            string basePath = Directory.GetCurrentDirectory(); // TODO: need check for it being in the Presentation layer else add //..//Ultrix.Presentation
            string environmentName = Environment.GetEnvironmentVariable(AspNetCoreEnvironment);
            return new ConfigurationBuilder()
                   .SetBasePath(basePath)
                   .AddJsonFile("appsettings.json")
                   .AddJsonFile("appsettings.Local.json", optional: true)
                   .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                   .AddEnvironmentVariables()
                   .Build();
        }
        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);
        public virtual TContext CreateDbContext(string[] args)
        {
            return args.Contains("UsePrecompiledConnectingString")
                ? Create(ConnectionString)
                : Create(BuildConfiguration());
        }
        private TContext Create(IConfiguration configuration)
        {
            SetConnectionString(configuration);
            return Create(ConnectionString);
        }
        private TContext Create(string connectionString)
        {
            if (OptionsBuilder == null)
                SetDbContextOptionsBuilder(connectionString);

            return CreateNewInstance(OptionsBuilder.Options);
        }
        private void SetDbContextOptionsBuilder(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty.", nameof(connectionString));
            }
            DbContextOptionsBuilder<TContext> optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlServer(connectionString);
            OptionsBuilder = optionsBuilder;
        }
    }
}
