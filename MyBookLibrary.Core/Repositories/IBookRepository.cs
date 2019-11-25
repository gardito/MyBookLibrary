using System.Collections.Generic;
using System.Threading.Tasks;
using MyBookLibrary.Core.Model;

namespace MyBookLibrary.Core.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetAllWithAuthorAsync();
        Task<Book> GetWithAuthorByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllWithAuthorByAuthorIdAsync(int authorId);
    }
}