using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBookLibrary.Core.Model;
using MyBookLibrary.Core.Repositories;
using System.Linq;

namespace MyBookLibrary.Data.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(MyBookLibraryDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Book>> GetAllWithAuthorAsync()
        {
            return await MyBookLibraryDbContext.Books
                .Include(b => b.Author)
                .ToListAsync();
            ;
        }

        public async Task<IEnumerable<Book>> GetAllWithAuthorByAuthorIdAsync(int authorId)
        {
            return await MyBookLibraryDbContext.Books
                .Include(b => b.Author)
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<Book> GetWithAuthorByIdAsync(int id)
        {
            return await MyBookLibraryDbContext.Books
                .Include(b => b.Author)
                .SingleOrDefaultAsync(b => b.Id == id);
        }

        private MyBookLibraryDbContext MyBookLibraryDbContext
        {
            get { return Context as MyBookLibraryDbContext; }
        }
    }
}