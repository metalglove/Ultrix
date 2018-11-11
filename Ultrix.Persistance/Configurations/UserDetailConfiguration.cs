using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities;

namespace Ultrix.Persistance.Configurations
{
    public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
    {
        public void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.HasKey("Id");
            builder.Property(p => p.TimestampCreated).HasDefaultValueSql("GetDate()");
        }
    }
}
