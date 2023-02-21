using Microsoft.EntityFrameworkCore;
using SampleAPI.Core.Entities;

namespace SampleAPI.Infrastructure.DAO;

internal sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Language> Languages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SeedLanguages();
    }
}
