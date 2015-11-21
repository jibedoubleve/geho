namespace Probel.Geho.Data.BusinessLogic.Hr
{
    using System.Linq;

    using Probel.Geho.Data.Database;
    using Probel.Geho.Data.Entities;

    class GroupAdapter
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

        internal void ClearForeignKeys(DataContext db)
        {
            while (Group.Persons.Count > 0) { Group.Persons.RemoveAt(0); }

            var days = (from d in db.Days
                        where d.Group.Id == this.Group.Id
                        select d);
            foreach (var d in days) { d.Group = null; }

            db.SaveChanges();
        }

        #endregion Methods
    }
}