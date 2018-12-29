using Microsoft.EntityFrameworkCore;
using Ultrix.Application.Interfaces;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Infrastructure
{
    public class ApplicationDbFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>, IFactory<ApplicationDbContext> 
    {
        private static readonly string[] UsePrecompiledConnectionStringArguments = new string[]{ "UsePrecompiledConnectingString" };

        public ApplicationDbFactory(string connectionStringName = "ApplicationDatabase") : base(connectionStringName, true)
        {
        }

        protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
        {
            return new ApplicationDbContext(options);
        }
        public string GetConnectionString()
        {
            return ConnectionString;
        }
        public ApplicationDbContext Create()
        {
            return base.CreateDbContext(UsePrecompiledConnectionStringArguments);
        }
    }
}
