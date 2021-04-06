using ServicesStore.Api.CartService.RemoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.CartService.RemoteInterfaces
{
    public interface IBooksService
    {
        Task<(bool result,BookRemote book, string errorMessage)> GetBook(Guid guid);
    }
}
