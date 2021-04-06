using AutoMapper;
using ServicesStore.Api.Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Book.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LibraryMaterial, LibraryMaterialDto>();
        }
    }
}
