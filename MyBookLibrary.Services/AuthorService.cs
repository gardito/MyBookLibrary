using System.Collections.Generic;
using System.Threading.Tasks;
using MyBookLibrary.Core;
using MyBookLibrary.Core.Model;
using MyBookLibrary.Core.Services;

namespace MyBookLibrary.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Author> CreateAuthor(Author newAuthor)
        {
            await _unitOfWork.Authors.AddAsync(newAuthor);
            await _unitOfWork.CommitAsync();
            return newAuthor;
        }

        public async Task DeleteAuthor(Author author)
        {
            _unitOfWork.Authors.Remove(author);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _unitOfWork.Authors.GetAllAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _unitOfWork.Authors.GetByIdAsync(id);
        }

        public async Task UpdateAuthor(Author authorToBeUpdated, Author author)
        {
            authorToBeUpdated.Name = author.Name;
            await _unitOfWork.CommitAsync();
        }
    }
}