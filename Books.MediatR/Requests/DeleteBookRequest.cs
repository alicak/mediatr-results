using System;
using System.Threading;
using System.Threading.Tasks;
using Books.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Books.MediatR.Requests
{
    public class DeleteBookRequest : IRequest<Result>
    {
        public int Id { get; set; }
    }

    public class DeleteBookRequestHandler : IRequestHandler<DeleteBookRequest, Result>
    {
        private BooksContext dbContext;

        public DeleteBookRequestHandler(BooksContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                return Result.Failure(ErrorCode.BadRequest);

            var book = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == request.Id);

            if (book is null)
                return Result.Failure(ErrorCode.NotFound);

            dbContext.Remove(book);

            await dbContext.SaveChangesAsync();

            return Result.Success();
        }
    }
}
