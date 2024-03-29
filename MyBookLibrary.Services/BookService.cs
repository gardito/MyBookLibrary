using System.Collections.Generic;
using System.Threading.Tasks;
using MyBookLibrary.Core;
using MyBookLibrary.Core.Model;
using MyBookLibrary.Core.Services;

namespace MyBookLibrary.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Book> CreateBook(Book newBook)
        {
            await _unitOfWork.Books.AddAsync(newBook);
            await _unitOfWork.CommitAsync();
            return newBook;
        }

        public async Task DeleteBook(Book book)
        {
            _unitOfWork.Books.Remove(book);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Book>> GetAllWithAuthor()
        {
            return await _unitOfWork.Books.GetAllWithAuthorAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _unitOfWork.Books.GetWithAuthorByIdAsync(id);
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorId(int authorId)
        {
            return await _unitOfWork.Books.GetAllWithAuthorByAuthorIdAsync(authorId);
        }

        public async Task UpdateBook(Book bookToBeUpdated, Book book)
        {
            bookToBeUpdated.Title = book.Title;
            bookToBeUpdated.Author = book.Author;

            await _unitOfWork.CommitAsync();
        }
    }
}