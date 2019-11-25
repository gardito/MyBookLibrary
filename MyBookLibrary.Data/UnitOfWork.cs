using System.Threading.Tasks;
using MyBookLibrary.Core;
using MyBookLibrary.Core.Repositories;
using MyBookLibrary.Data.Repositories;

namespace MyBookLibrary.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyBookLibraryDbContext _context;
        private BookRepository _bookRepository;
        private AuthorRepository _authorRepository;

        public UnitOfWork(MyBookLibraryDbContext context)
        {
            _context = context;
        }

        public IBookRepository Books => _bookRepository = _bookRepository ?? new BookRepository(_context);

        public IAuthorRepository Authors => _authorRepository = _authorRepository ?? new AuthorRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}