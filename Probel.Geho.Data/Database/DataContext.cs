namespace Probel.Geho.Services.Database
{
    using System;
    using System.Data.Entity;
    using System.IO;

    using Probel.Geho.Services.Entities;

    internal class DataContext : DbContext
    {
        #region Constructors

        public DataContext()
            : base("DatabaseConnection")
        {
            CheckDirectory();
            if (DataCfg.EraseDatabase) { Database.SetInitializer(new DropCreateDatabaseAlways<DataContext>()); }
            else { Database.CreateIfNotExists(); }
        }

        #endregion Constructors

        #region Properties

        public DbSet<Absence> Absences
        {
            get;
            set;
        }

        public DbSet<Activity> Activities
        {
            get; set;
        }

        public DbSet<Day> Days
        {
            get; set;
        }

        public DbSet<Group> Groups
        {
            get;
            set;
        }

        public DbSet<LunchTime> LunchTimes
        {
            get; set;
        }

        public DbSet<Person> People
        {
            get;
            set;
        }

        public DbSet<Week> Weeks
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        private static void CheckDirectory()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Probel\Geho");
            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        #endregion Methods
    }
}