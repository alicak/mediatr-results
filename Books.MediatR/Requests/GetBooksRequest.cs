using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Books.Data;
using Books.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Books.MediatR.Requests
{
    public class GetBooksRequest : IRequest<Result<IEnumerable<Book>>>
    {
        
    }

    public class GetBooksRequestHandler : IRequestHandler<GetBooksRequest, Result<IEnumerable<Book>>>
    {
        private BooksContext dbContext;

        public GetBooksRequestHandler(BooksContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<Book>>> Handle(GetBooksRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<Book> books = await dbContext.Books.ToListAsync();

            return Result.Success(books);
        }
    }
}
