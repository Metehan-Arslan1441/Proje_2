using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System;
namespace proje1.Models.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opts) : base(opts)
        {

        }
        public DbSet<Person> persons { get; set; }
    }

}
