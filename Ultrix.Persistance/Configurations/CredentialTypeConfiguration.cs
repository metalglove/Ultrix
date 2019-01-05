using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Domain.Enumerations;

namespace Ultrix.Persistance.Configurations
{
    public class CredentialTypeConfiguration : IEntityTypeConfiguration<CredentialType>
    {
        public void Configure(EntityTypeBuilder<CredentialType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Code).IsRequired().HasMaxLength(32);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(64);
        }
    }
}
