using AutoMapper;
using ServicesStore.Api.Book.DTOs;
using ServicesStore.Api.Book.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesStore.Api.BooksService.Tests
{
    public class MappingTest:Profile
    {
        public MappingTest()
        {
            CreateMap<LibraryMaterial,LibraryMaterialDto>();
        }
    }
}
