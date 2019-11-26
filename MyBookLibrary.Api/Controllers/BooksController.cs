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
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _bookService.GetAllWithAuthor();
            var booksDto = _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(books);
            return Ok(books);
        }
    }
}