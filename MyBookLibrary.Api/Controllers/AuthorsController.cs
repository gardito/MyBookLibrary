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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthors();

            var authorsDto = _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDto>>(authors);

            return Ok(authorsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorById(int id)
        {
            if(id == 0)
                return BadRequest();

            var author = await _authorService.GetAuthorById(id);

            if(author == null)
                return NotFound();

            var authorDto = _mapper.Map<Author, AuthorDto>(author);

            return authorDto;
        }

        [HttpPost("")]
        public async Task<ActionResult<AuthorDto>> CreateAuthor([FromBody] SaveAuthorDto createAuthorDto)
        {
            if(createAuthorDto == null)
                return BadRequest();

            var authorToCreate = _mapper.Map<SaveAuthorDto, Author>(createAuthorDto);

            var newAuthor = await _authorService.CreateAuthor(authorToCreate);

            var author = await _authorService.GetAuthorById(newAuthor.Id);

            var authorCreated = _mapper.Map<Author, AuthorDto>(author);

            return Ok(authorCreated);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<AuthorDto>> UpdateAuthor(int id, [FromBody] SaveAuthorDto updateAuthorDto)
        {
            if(id == 0)
                return BadRequest();
            
            if (updateAuthorDto == null)
                return BadRequest();

            var authorToUpdate = await _authorService.GetAuthorById(id);

            if (authorToUpdate == null)
                return NotFound();

            var newAuthor = _mapper.Map<SaveAuthorDto, Author>(updateAuthorDto);

            await _authorService.UpdateAuthor(authorToUpdate, newAuthor);

            var updatedAuthor = await _authorService.GetAuthorById(id);

            var updatedAuthorDto = _mapper.Map<Author, AuthorDto>(updatedAuthor);

            return Ok(updatedAuthorDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeteleAuthor(int id)
        {
            if (id == 0)
                return BadRequest();

            var authorToDelete = await _authorService.GetAuthorById(id);

            await _authorService.DeleteAuthor(authorToDelete);

            return NoContent();
        }
    }
}