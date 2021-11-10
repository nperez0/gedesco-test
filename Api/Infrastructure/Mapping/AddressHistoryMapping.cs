using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infrastructure.Mapping
{
    public class AddressHistoryMapping : IEntityTypeConfiguration<AddressHistory>
    {
        public void Configure(EntityTypeBuilder<AddressHistory> builder)
        {
            builder.HasKey(x => x.AddressId);

            builder.Property(x => x.AddressId)
                .ValueGeneratedNever();
        }
    }
}
