using Microsoft.EntityFrameworkCore;
using MyBookLibrary.Core.Model;
using MyBookLibrary.Data.Configurations;

namespace MyBookLibrary.Data
{
    public class MyBookLibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public MyBookLibraryDbContext(DbContextOptions<MyBookLibraryDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new AuthorConfiguration());

            builder
                .ApplyConfiguration(new BookConfiguration());
        }

    }
}