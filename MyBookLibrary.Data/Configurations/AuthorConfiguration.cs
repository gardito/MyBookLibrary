using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBookLibrary.Core.Model;

namespace MyBookLibrary.Data.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Id)
                .UseIdentityColumn();

            builder
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .ToTable("Authors");
        }
    }
}