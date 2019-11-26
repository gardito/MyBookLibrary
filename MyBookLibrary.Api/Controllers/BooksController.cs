using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBookLibrary.Core.Model;
using MyBookLibrary.Core.Services;

namespace MyBookLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
        }
        
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllMusics()
        {
            var books = await _bookService.GetAllWithAuthor();
            return Ok(books);
        }
    }
}