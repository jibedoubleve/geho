namespace Probel.Geho.Data.BusinessLogic
{
    using System;
    using System.Collections.Generic;

    using Probel.Geho.Data.Dto;

    public interface IScheduleService
    {
        #region Methods

        void CreateWeek(DateTime dateInWeek);

        void FeedDay(DateTime date, IEnumerable<PersonDto> educators, GroupDto group, bool isMorning);

        void FeedDay(DateTime date, GroupDto group, bool isMorning);

        IEnumerable<PersonDto> GetFreeEducators(DateTime date);

        GroupDto GetGroup(int id);

        WeekDto GetWeek(DateTime dateInWeek);

        WeekDto GetWeek(int id);

        IEnumerable<WeekDto> GetWeeks();

        bool WeekExists(DateTime date);

        #endregion Methods
    }
}