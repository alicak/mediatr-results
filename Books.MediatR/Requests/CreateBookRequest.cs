using System.Threading;
using System.Threading.Tasks;
using Books.Data;
using Books.Data.Entities;
using MediatR;

namespace Books.MediatR.Requests
{
    public class CreateBookRequest : IRequest<Result<Book>>
    {
        public Book Book { get; set; }
    }

    public class CreateBookRequestHandler : IRequestHandler<CreateBookRequest, Result<Book>>
    {
        private BooksContext dbContext;

        public CreateBookRequestHandler(BooksContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task<Result<Book>> Handle(CreateBookRequest request, CancellationToken cancellationToken)
        {
            var book = request.Book;

            dbContext.Add(book);

            await dbContext.SaveChangesAsync();

            return Result.Success(book);
        }
    }
}
