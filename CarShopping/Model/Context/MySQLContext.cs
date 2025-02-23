using Microsoft.EntityFrameworkCore;

namespace CarShopping.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() { }
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CarDealer>()
                .HasMany(x => x.Cars)
                .WithOne(o => o.Owner)
                .HasForeignKey(o => o.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Car>()
                .HasOne(o => o.Owner)
                .WithMany(x => x.Cars)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }




        public DbSet<Car> Car { get; set; }
        public DbSet<CarDealer> CarDealer { get; set; }
        //preciso fazer muitos pra muitos, pois muitas peças podem servir em muitas marcas, e depois muitas peças podem servir em muitos modelos.
        //public DbSet<CarParts> CarPars { get; set; }
    }
}
