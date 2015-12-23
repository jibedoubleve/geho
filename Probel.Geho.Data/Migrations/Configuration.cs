namespace Probel.Geho.Data.Migrations
{
    using System.Data.Entity.Migrations;

    using Database;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        #region Constructors

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Probel.Geho.Data.Database.DataContext";
        }

        #endregion Constructors

        #region Methods

        protected override void Seed(DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        #endregion Methods
    }
}