using Microsoft.EntityFrameworkCore;

namespace CarShopping.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() { }
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }
        public DbSet<Car> Car { get; set; }
        public DbSet<CarDealer> CarDealer { get; set; }
    }
}
