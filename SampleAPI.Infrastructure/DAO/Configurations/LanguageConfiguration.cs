using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleAPI.Core.Entities;

namespace SampleAPI.Infrastructure.DAO.Configurations;
internal sealed class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasIndex(b => b.Id);

        builder.Property(b => b.Code).
            IsRequired().
            HasMaxLength(2);

        builder.Property(b => b.FullName).
            IsRequired().
            HasMaxLength(50);
    }
}