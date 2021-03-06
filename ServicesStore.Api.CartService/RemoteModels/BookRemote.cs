using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.CartService.RemoteModels
{
    public class BookRemote
    {
        public Guid? LibraryMaterialId { get; set; }

        public string Title { get; set; }

        public DateTime? PublishDate { get; set; }

        public Guid? BookAuthorGuid { get; set; }
    }
}
