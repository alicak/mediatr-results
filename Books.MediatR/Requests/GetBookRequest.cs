using System.Threading;
using System.Threading.Tasks;
using Books.Data;
using Books.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Books.MediatR.Requests
{
    public class GetBookRequest : IRequest<Result<Book>>
    {
        public int Id { get; set; }
    }

    public class GetBookRequestHandler : IRequestHandler<GetBookRequest, Result<Book>>
    {
        private BooksContext dbContext;

        public GetBookRequestHandler(BooksContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result<Book>> Handle(GetBookRequest request, CancellationToken cancellationToken)
        {
            var book = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == request.Id);

            if (book is null)
                return Result.Failure<Book>(ErrorCode.NotFound);

            return Result.Success(book);
        }
    }
}
