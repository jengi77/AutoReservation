﻿using System.Collections.Generic;
using CarReservation.Dal.Entities;
using CarReservation.Dal.Migrations;
using System.Data.Entity;

namespace CarReservation.Dal
{
    public class CarReservationContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public CarReservationContext()
        {
            // Ensures that the database will be initialized
            Database.Initialize(false);

            // Disable lazy loading
            Configuration.LazyLoadingEnabled = false;

            // ----------------------------------------------------------------------------------------------------
            // Choose one of these three options:

            // Use for real "database first"
            //      - Database will NOT be created by Entity Framework
            //      - Database will NOT be modified by Entity Framework
            // Database.SetInitializer<AutoReservationContext>(null);

            // Use this for initial "code first" 
            //      - Database will be created by Entity Framework
            //      - Database will NOT be modified by Entity Framework
            // Database.SetInitializer(new CreateDatabaseIfNotExists<AutoReservationContext>());

            // Use this for real "code first" 
            //      - Database will be created by Entity Framework
            //      - Database will be modified by Entity Framework
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CarReservationContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                        .Map<LuxuryCar>(m => m.Requires("CarClass").HasValue(0));

            modelBuilder.Entity<Car>()
                        .Map<StandardCar>(m => m.Requires("CarClass").HasValue(2));

            modelBuilder.Entity<Car>()
                        .Map<MidRangeCar>(m => m.Requires("CarClass").HasValue(1));
            base.OnModelCreating(modelBuilder);
        }
    }
}


