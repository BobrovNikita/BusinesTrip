using Azure.Core;
using DBCourse_Azuavchikova.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DBCourse_Azuavchikova.Data
{
    public class BusinesTripsPayments : DbContext
    {
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<BusinesTrip> BusinesTrips { get; set; }
        public DbSet<TravelExpenses> TravelExpenses { get; set; }
        public DbSet<TypesTravelExpenses> TypesTravelExpenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=BusinesTripsPayments;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Employee
            modelBuilder
                .Entity<Employee>()
                .HasOne(e => e.Position)
                .WithMany(e => e.Employees)
                .OnDelete(DeleteBehavior.NoAction);

            //Busines trip
            modelBuilder
                .Entity<BusinesTrip>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.BusinesTrips)
                .OnDelete(DeleteBehavior.NoAction);

            //Travel expenses
            modelBuilder
                .Entity<TravelExpenses>()
                .HasOne(e => e.BusinesTrip)
                .WithMany(e => e.TravelExpenses)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<TravelExpenses>()
                .HasOne(e => e.TypesTravelExpenses)
                .WithMany(e => e.TravelExpenses)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
