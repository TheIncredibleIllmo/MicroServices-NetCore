using AutoMapper;
using ServicesStore.Api.Author.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Author.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookAuthor, BookAuthorDto>();
        }
    }
}
