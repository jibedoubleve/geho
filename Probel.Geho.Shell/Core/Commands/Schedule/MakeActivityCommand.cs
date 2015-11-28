namespace Probel.Geho.Shell.Core.Commands.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Data.Entities;

    using Probel.Geho.Data.Dto;

    [Command("make-activity", "mkac", "Make a new activity", "")]
    public class MakeActivityCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            var dayStr = ArgProcessor.Get("day");
            var day = ArgProcessor.ParseDayOfWeel(dayStr);

            var isMorningStr = ArgProcessor.Get("isMorning");
            var isMorning = (isMorningStr.ToLower() == "morning" || isMorningStr.ToLower() == "m");

            var name = ArgProcessor.Get("name");

            var idStr = ArgProcessor.Get("ide").Split(';');
            var ide = ArgProcessor.ParseIdCollection(idStr);

            var people = new List<PersonDto>();
            foreach (var i in ide)
            {
                people.Add(HrManager.GetPerson(i));
            }

            HrManager.CreateActivity(day, isMorning ? MomentDay.Morning : MomentDay.Afternoon, people, name);
        }

        #endregion Methods
    }
}