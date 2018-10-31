using Microsoft.EntityFrameworkCore;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Infrastructure
{
    public class ApplicationDbFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
    {
        protected override string ConnectionStringName => "ApplicationDatabase";
        private static readonly string[] UsePrecompiledConnectionStringArguments = new string[]{ "UsePrecompiledConnectingString" };

        public ApplicationDbFactory() : base(true)
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
        public ApplicationDbContext CreateNewInstance()
        {
            return base.CreateDbContext(UsePrecompiledConnectionStringArguments);
        }
    }
}
