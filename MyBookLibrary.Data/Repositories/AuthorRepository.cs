using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBookLibrary.Core.Model;
using MyBookLibrary.Core.Repositories;

namespace MyBookLibrary.Data.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(MyBookLibraryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Author>> GetAllWithBooksAsync()
        {
            return await MyBookLibraryDbContext.Authors
                .Include(a => a.Books)
                .ToListAsync();
        }
        
        public async Task<Author> GetWithBooksByIdAsync(int id)
        {
            return await MyBookLibraryDbContext.Authors
                .Include(a => a.Books)
                .SingleOrDefaultAsync(a => a.Id ==id);
        }

        private MyBookLibraryDbContext MyBookLibraryDbContext
        {
            get { return Context as MyBookLibraryDbContext; }
        }

    }
}