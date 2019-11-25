using System.Collections.Generic;
using System.Threading.Tasks;
using MyBookLibrary.Core.Model;

namespace MyBookLibrary.Core.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(int id);
        Task<Author> CreateAuthor(Author newAuthor);
        Task UpdateAuthor(Author authorToBeUpdated, Author author);
        Task DeleteAuthor(Author author);
    }
}