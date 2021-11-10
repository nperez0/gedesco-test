using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.EF.Mapping
{
    internal class EventEntryMapping : IEntityTypeConfiguration<EventEntry>
    {
        public void Configure(EntityTypeBuilder<EventEntry> builder)
        {
            builder.HasKey(x => x.EventId);

            builder.Property(x => x.AggregateId);
            builder.Property(x => x.EventTypeName);
            builder.Property(x => x.CreationTime);
            builder.Property(x => x.Content);
            builder.Property(x => x.Version);
        }
    }
}
