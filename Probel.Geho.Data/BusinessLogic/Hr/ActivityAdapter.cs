namespace Probel.Geho.Data.BusinessLogic.Hr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Probel.Geho.Data.Database;
    using Probel.Geho.Data.Entities;

    internal class ActivityAdapter
    {
        #region Fields

        private readonly Activity Activity;

        #endregion Fields

        #region Constructors

        public ActivityAdapter(Activity activity)
        {
            this.Activity = activity;
        }

        #endregion Constructors

        #region Methods

        public void ClearForeignKeys(DataContext db)
        {
            if (Activity.People != null)
            {
                while (Activity.People.Count > 0) { Activity.People.RemoveAt(0); }
            }
            db.SaveChanges();
        }

        #endregion Methods
    }
}