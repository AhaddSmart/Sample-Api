using Domain.Entities.Sample;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class AboutConfiguration
{
    public void Configure(EntityTypeBuilder<About> builder)
    {
        builder.Property(x => x.text)
            .IsRequired();
    }
}
