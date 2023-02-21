using Microsoft.EntityFrameworkCore;
using SampleAPI.Core.Entities;

namespace SampleAPI.Infrastructure.DAO;

internal static class LanguageSeeder
{
    public static void SeedLanguages(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>().HasData
        (
            new Language { Id = 1, FullName = "Niemiecki", Code = "DE" },
            new Language { Id = 2, FullName = "Angielski", Code = "EN" }
        );
    }
}
