using System.Collections.Generic;
using System.Threading.Tasks;
using MyBookLibrary.Core.Model;

namespace MyBookLibrary.Core.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAllWithBooksAsync();
        Task<Author> GetWithBooksByIdAsync(int id);
    }
}