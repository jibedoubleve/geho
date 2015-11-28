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

        IEnumerable<PersonDto> GetEducatorsBusyInDay(DateTime currentDay, bool isMorning);

        IEnumerable<PersonDto> GetFreeBeneficiaries(GroupDto group, DateTime currentDay, bool isMorning);

        IEnumerable<PersonDto> GetFreeEducators(DateTime currentDay, bool isMorning);

        GroupDto GetGroup(int id);

        IEnumerable<GroupDto> GetGroups();

        WeekDto GetWeek(DateTime dateInWeek);

        WeekDto GetWeek(int id);

        IEnumerable<WeekDto> GetWeeks();

        bool WeekExists(DateTime date);

        #endregion Methods
    }
}