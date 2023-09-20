using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thrima.Domain.Books;

namespace Thrima.Infrastructure.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .Property(x => x.Id)
            .UseIdentityColumn();
        
        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(256);
        
        builder
            .Property(x => x.Author)
            .IsRequired()
            .HasMaxLength(256);
    }
}
