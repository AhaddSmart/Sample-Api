﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<City> Cities { get; }
        DbSet<About> Abouts { get; }
        DbSet<News> News { get; }
        DbSet<Category> Categories{ get; }
        DbSet<Offer> Offers{ get; }
        DbSet<Banner> Banners { get; }
        DbSet<FileRepo> FileRepos { get; }
        DbSet<Vendor> Vendors { get; }
        DbSet<BusinessType> BusinessTypes{ get; }
        DbSet<LogEntry> LogEntries { get; }
        DbSet<ExceptionLog> ExceptionLogs { get; }
        DbSet<Enquiry> Enquiries { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
