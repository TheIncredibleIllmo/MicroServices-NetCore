using ServicesStore.Api.Gateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Gateway.Interfaces
{
    public interface IAuthorService
    {
        Task<(bool result, AuthorRemote author, string )> GetAuthor(Guid id);
    }
}
