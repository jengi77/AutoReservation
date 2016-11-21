using System.Data.Entity.Migrations;

namespace CarReservation.Dal.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AutoReservationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = typeof(AutoReservationContext).FullName;
        }
    }
}
