using DataAccess.Models;
using Microsoft.EntityFrameworkCore;


namespace DataAccess
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName:"Payments");
            
        }
        public DbSet<Card> Card { get; set; }
        public DbSet<Balance> Balance { get; set; }
    }
}
