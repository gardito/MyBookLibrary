using AutoMapper;
using MyBookLibrary.Api.DTOs;
using MyBookLibrary.Core.Model;

namespace MyBookLibrary.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<Author, AuthorDto>();

            CreateMap<BookDto, Book>();
            CreateMap<AuthorDto, Author>();
        }
    }
}