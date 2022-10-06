using HellowWorldApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HellowWorldApi.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}
