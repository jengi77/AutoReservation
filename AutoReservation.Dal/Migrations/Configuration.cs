using System.Data.Entity.Migrations;

namespace CarReservation.Dal.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CarReservationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = typeof(CarReservationContext).FullName;
        }
    }
}
