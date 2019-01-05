using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Persistance.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(e => new { e.RoleId, e.PermissionId });
        }
    }
}
