using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Data;
using Books.Data.Entities;
using Books.MediatR;
using Books.MediatR.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : AbstractControllerBase
    {
        private readonly BooksContext dbContext;

        private readonly IMediator mediator;

        public BooksController(BooksContext dbContext, IMediator mediator)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            var result = await mediator.Send(new GetBooksRequest());

            if (result.IsSuccess)
            {
                var books = result.Body;
                return Ok(books);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var result = await mediator.Send(new GetBookRequest { Id = id });

            if (result.IsSuccess)
            {
                var book = result.Body;
                return Ok(book);
            }

            if (result.ErrorCode == ErrorCode.NotFound)
                return NotFound();

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Create(Book book)
        {
            var result = await mediator.Send(new CreateBookRequest { Book = book });

            if (result.IsSuccess)
            {
                var newBook = result.Body;
                return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, Book newBook)
        {
            var result = await mediator.Send(new EditBookRequest { Id = id, Book = newBook });

            if (result.IsSuccess)
                return NoContent();

            if (result.ErrorCode == ErrorCode.NotFound)
                return NotFound();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteBookRequest { Id = id });

            if (result.IsSuccess)
                return NoContent();

            if (result.ErrorCode == ErrorCode.BadRequest)
                return BadRequest();

            if (result.ErrorCode == ErrorCode.NotFound)
                return NotFound();

            return BadRequest();
        }
    }
}
