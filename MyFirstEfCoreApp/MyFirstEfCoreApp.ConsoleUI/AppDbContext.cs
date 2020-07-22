using Microsoft.EntityFrameworkCore;

namespace MyFirstEfCoreApp.ConsoleUI
{
    public class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public AppDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}