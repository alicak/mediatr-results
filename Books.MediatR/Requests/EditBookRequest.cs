using System.Threading;
using System.Threading.Tasks;
using Books.Data;
using Books.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Books.MediatR.Requests
{
    public class EditBookRequest : IRequest<Result>
    {
        public int Id { get; set; }

        public Book Book { get; set; }
    }

    public class EditBookRequestHandler : IRequestHandler<EditBookRequest, Result>
    {
        private BooksContext dbContext;

        public EditBookRequestHandler(BooksContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result> Handle(EditBookRequest request, CancellationToken cancellationToken)
        {
            var book = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == request.Id);

            if (book is null)
                return Result.Failure(ErrorCode.NotFound);

            var updatedBook = request.Book;

            book.Name = updatedBook.Name;
            book.Author = updatedBook.Author;
            book.YearPublished = updatedBook.YearPublished;

            await dbContext.SaveChangesAsync();

            return Result.Success();
        }
    }
}
