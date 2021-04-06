using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesStore.Api.BooksService.Tests
{
    public class AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public T Current => _enumerator.Current;

        public AsyncEnumerator(IEnumerator<T> enumerator) => _enumerator = enumerator ?? throw new ArgumentNullException();


        public async ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            return await Task.FromResult(_enumerator.MoveNext());
        }
    }
}
