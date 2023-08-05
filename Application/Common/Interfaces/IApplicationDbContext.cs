using Domain.Entities.Sample;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<City> Cities { get; }
        DbSet<About> Abouts { get; }
        DbSet<News> News { get; }
        DbSet<Category> Categories{ get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
