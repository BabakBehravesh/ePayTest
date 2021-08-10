using Microsoft.EntityFrameworkCore;
using ePay.ApplicationCore.Models;

namespace ePay.Infrastructure.Data
{
    public class ePayContext : DbContext
    {
        public ePayContext(DbContextOptions<ePayContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
        }
    }
}
