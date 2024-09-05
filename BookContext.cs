using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using BookStoreApp.Models;

namespace BookStoreApp.Data{
    public class BookContext:DbContext {
        public BookContext(DbContextOptions<BookContext> options):base(options) {} 
        public DbSet<Book> Books { get; set; }
        

    }
}