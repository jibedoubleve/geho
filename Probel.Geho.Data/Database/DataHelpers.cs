namespace Probel.Geho.Services.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Services.Database;

    public class DataHelpers
    {
        #region Methods

        public void ClearData()
        {
            using (var db = new DataContext())
            {
                db.Absences.RemoveRange(db.Absences);
                db.Groups.RemoveRange(db.Groups);
                db.People.RemoveRange(db.People);
                db.Days.RemoveRange(db.Days);
                db.Weeks.RemoveRange(db.Weeks);
                db.Activities.RemoveRange(db.Activities);

                db.SaveChanges();
            }
        }

        #endregion Methods
    }
}