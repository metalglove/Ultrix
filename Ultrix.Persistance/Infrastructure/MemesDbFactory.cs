using Microsoft.EntityFrameworkCore;
using Ultrix.Persistance.Repositories;

namespace Ultrix.Persistance.Infrastructure
{
    public class MemesDbFactory : DesignTimeDbContextFactoryBase<MemesDbContext>
    {
        protected override string ConnectionStringName => "MemesDatabase";
        private static readonly string[] UsePrecompiledConnectionStringArguments = new string[]{ "UsePrecompiledConnectingString" };

        public MemesDbFactory() : base(true)
        {
            
        }

        protected override MemesDbContext CreateNewInstance(DbContextOptions<MemesDbContext> options)
        {
            return new MemesDbContext(options);
        }

        public MemesDbContext CreateNewInstance()
        {
            return base.CreateDbContext(UsePrecompiledConnectionStringArguments);
        }
    }
}
