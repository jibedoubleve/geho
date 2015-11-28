namespace Probel.Geho.Shell.Core.Commands.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Probel.Geho.Data.Business_Logic.Schedule;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Shell.Helpers;

    [Command("list-week", "lsw", "List weeks", "lsw -id:[id] | -date:[date]")]
    public class ListWeekCommand : BaseCommand
    {
        #region Fields

        private const int COLUMN = 30;

        private readonly ConsoleJumper CJ = new ConsoleJumper(10);

        #endregion Fields

        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            Output.ResetMaxCursorTop();
            Output.InWhite.WriteLine("Display weeks...");

            if (this.ArgProcessor.Has("date"))
            {
                var dateStr = this.ArgProcessor.Get("date");
                var date = this.ArgProcessor.ParseDate(dateStr);

                var week = ScheduleManager.GetWeek(date);

                DisplayWeek(week);
            }
            else
            {
                var weeks = ScheduleManager.GetWeeks();
                foreach (var week in weeks)
                {
                    DisplayWeek(week);
                }
            }
            Console.CursorTop = Output.MaxCursorTop;
        }

        private void DisplayDays(IEnumerable<DayDto> days, int offset, int? top = null)
        {
            Console.CursorTop = (top.HasValue) ? top.Value : Console.CursorTop;
            foreach (var day in days)
            {
                Output.Yellow(offset).WriteLine("{0} - {1}",
                    day.Date.DayOfWeek,
                    day.IsMorning ? "MORNING" : "AFTERNOON");
                Output.DarkYellow(offset).WriteLine("{0}", day.Group.Name);
                foreach (var e in day.People) { Output.White(offset).WriteLine("{0} {1}", e.Name, e.Surname); }
            }
        }

        private void DisplayWeek(WeekDto week)
        {
            Console.CursorTop = Output.MaxCursorTop + 3;
            if (Console.BufferWidth < 5 * COLUMN)
            {
                Console.SetBufferSize(8 * COLUMN, Console.BufferHeight);
                return;
            }
            Output.InYellow.WriteLine("Week: {0}", week.MondayDate.ToShortDateString());
            Output.InYellow.WriteLine("-----------------");
            var a = new WeekAdapter(week);

            var offset = 0;
            var top = Console.CursorTop;
            DisplayDays(a.GetDay(DayOfWeek.Monday), offset, top);
            DisplayDays(a.GetDay(DayOfWeek.Monday), offset);
            offset += COLUMN;
            DisplayDays(a.GetDay(DayOfWeek.Tuesday), offset, top);
            DisplayDays(a.GetDay(DayOfWeek.Tuesday), offset);
            offset += COLUMN;
            DisplayDays(a.GetDay(DayOfWeek.Wednesday), offset, top);
            DisplayDays(a.GetDay(DayOfWeek.Wednesday), offset);
            offset += COLUMN;
            DisplayDays(a.GetDay(DayOfWeek.Thursday), offset, top);
            DisplayDays(a.GetDay(DayOfWeek.Thursday), offset);
            offset += COLUMN;
            DisplayDays(a.GetDay(DayOfWeek.Friday), offset, top);
            DisplayDays(a.GetDay(DayOfWeek.Friday), offset);
            offset += COLUMN;

            Console.CursorTop += 1;
        }

        #endregion Methods
    }
}