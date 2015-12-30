namespace Probel.Geho.Gui.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Probel.Geho.Services.Entities;

    using Services.Dto;

    public class ActivityModel
    {
        #region Properties

        public string Beneficiaries
        {
            get; set;
        }

        public DayOfWeek DayOfWeek
        {
            get; set;
        }

        public string Educators
        {
            get; set;
        }

        public MomentDay MomentDay
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }

    public static class ActivityModelExtensions
    {
        #region Methods

        public static List<ActivityModel> ToModels(this IEnumerable<ActivityDto> list)
        {
            var result = new List<ActivityModel>();
            foreach (var item in list)
            {
                result.Add(new ActivityModel()
                {
                    Name = item.Name,
                    DayOfWeek = item.DayOfWeek,
                    MomentDay = item.MomentDay,
                    Beneficiaries = Get(item.People, isEducator: false),
                    Educators = Get(item.People, isEducator: true),
                });
            }
            return result.OrderBy(e => e.DayOfWeek).ThenBy(e => e.MomentDay).ToList();
        }

        private static string Get(IEnumerable<PersonDto> people, bool isEducator)
        {
            var educators = (from p in people
                             where p.IsEducator == isEducator
                             select new { p.Name, p.Surname });

            var sb = new StringBuilder();
            foreach (var educator in educators)
            {
                sb.AppendFormat("{0} {1}, ", educator.Name.Trim(), educator.Surname.Trim());
            }
            var l = (", ".Length);
            sb.Remove(sb.Length - l, l);
            return sb.ToString();
        }

        #endregion Methods
    }
}