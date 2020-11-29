using Books.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.Data
{
    public class BooksContext : DbContext
    {
    	public BooksContext(DbContextOptions<BooksContext> options) : base(options) { }
    	
        public DbSet<Book> Books { get; set; }
    }
}
