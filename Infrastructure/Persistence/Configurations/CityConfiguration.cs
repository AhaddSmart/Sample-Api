using Domain.Entities.Sample;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class CityConfiguration
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        //builder.Ignore(e => e.DomainEvents);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
