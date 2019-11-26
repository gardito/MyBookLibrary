using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBookLibrary.Api.DTOs;
using MyBookLibrary.Core.Model;
using MyBookLibrary.Core.Services;

namespace MyBookLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }
        
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllWithAuthor();
            var booksDto = _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(books);
            return Ok(booksDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _bookService.GetBookById(id);
            var bookDto = _mapper.Map<Book, BookDto>(book);
            
            return Ok(bookDto);
        }

        [HttpPost("")]
        public async Task<ActionResult<BookDto>> CreateBook([FromBody] SaveBookDto saveBookDto)
        {
            var bookToCreate = _mapper.Map<SaveBookDto, Book>(saveBookDto);
            var newBook = await _bookService.CreateBook(bookToCreate);

            var book = await _bookService.GetBookById(newBook.Id);

            var bookDto = _mapper.Map<Book, BookDto>(book);

            return Ok(bookDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> UpdateBook(int id, [FromBody] SaveBookDto saveBookDto)
        {
            var bookToUpdate = await _bookService.GetBookById(id);

            if (bookToUpdate == null)
                return NotFound();

            var book = _mapper.Map<SaveBookDto, Book>(saveBookDto);

            await _bookService.UpdateBook(bookToUpdate, book);

            var updatedBook = await _bookService.GetBookById(id);

            var updatedBookDto = _mapper.Map<Book, SaveBookDto>(updatedBook);

            return Ok(updatedBookDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id == 0)
                return BadRequest();

            var music = await _bookService.GetBookById(id);

            if (music == null)
                return NotFound();

            await _bookService.DeleteBook(music);

            return NoContent();
        }
    }
}