namespace Probel.Geho.Services.BusinessLogic.Hr
{
    using System.Linq;

    using Probel.Geho.Services.Database;
    using Probel.Geho.Services.Entities;

    internal class GroupAdapter
    {
        #region Fields

        private readonly Group Group;

        #endregion Fields

        #region Constructors

        public GroupAdapter(Group group)
        {
            this.Group = group;
        }

        #endregion Constructors

        #region Methods

        public void ClearForeignKeys(DataContext db)
        {
            while (Group.People.Count > 0) { Group.People.RemoveAt(0); }

            var days = (from d in db.Days
                        where d.Group.Id == this.Group.Id
                        select d);
            foreach (var d in days) { db.Days.Remove(d);  }

            db.SaveChanges();
        }

        #endregion Methods
    }
}