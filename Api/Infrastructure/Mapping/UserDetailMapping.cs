using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infrastructure.Mapping
{
    public class UserDetailMapping : IEntityTypeConfiguration<UserDetail>
    {
        public void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId)
                .ValueGeneratedNever();

            builder.OwnsMany(x => x.Addresses)
                .HasKey(x => x.AddressId);

            builder.OwnsMany(x => x.Addresses)
                .Property(x => x.AddressId)
                .ValueGeneratedNever();

            builder.OwnsMany(x => x.Addresses)
                .Property(x => x.Street);

            builder.OwnsMany(x => x.Addresses)
                .Property(x => x.ZipCode);

            builder.OwnsMany(x => x.Addresses)
                .Property(x => x.City);

            builder.OwnsMany(x => x.Addresses)
                .Property(x => x.Country);
        }
    }
}
