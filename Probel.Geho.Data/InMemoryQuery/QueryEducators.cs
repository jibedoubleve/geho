namespace Probel.Geho.Data.InMemoryQuery
{
    using System.Collections.Generic;
    using System.Linq;

    using Probel.Geho.Data.Dto;

    public static class QueryPersons
    {
        #region Methods

        public static IEnumerable<PersonDto> GetBeneficiaries(this IEnumerable<PersonDto> list)
        {
            return (from i in list
                    where !i.IsEducator
                    orderby i.Name, i.Surname
                    select i).ToList();
        }

        public static PersonDto GetById(this IEnumerable<PersonDto> list, int id)
        {
            return (from e in list
                    where e.Id == id
                    select e).SingleOrDefault();
        }

        public static IEnumerable<PersonDto> GetEducators(this IEnumerable<PersonDto> list)
        {
            return (from i in list
                    where i.IsEducator
                    orderby i.Name, i.Surname
                    select i).ToList();
        }

        public static void OrderByDay(this PersonDto educator)
        {
            educator.Days = educator.Days.OrderBy(e => e.Date).ToList();
        }

        #endregion Methods
    }
}