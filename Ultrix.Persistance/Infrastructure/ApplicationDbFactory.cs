using Microsoft.EntityFrameworkCore;
using Ultrix.Application.Interfaces;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Infrastructure
{
    public class ApplicationDbFactory : DesignTimeDbContextFactoryBase<AppDbContext>, IFactory<AppDbContext> 
    {
        private static readonly string[] UsePrecompiledConnectionStringArguments = new string[]{ "UsePrecompiledConnectingString" };

        public ApplicationDbFactory() : base("ApplicationDatabase", true)
        {

        }
        public ApplicationDbFactory(string connectionStringName = "ApplicationDatabase") : base(connectionStringName, true)
        {
        }

        protected override AppDbContext CreateNewInstance(DbContextOptions<AppDbContext> options)
        {
            return new AppDbContext(options);
        }
        public AppDbContext Create()
        {
            return base.CreateDbContext(UsePrecompiledConnectionStringArguments);
        }
    }
}
