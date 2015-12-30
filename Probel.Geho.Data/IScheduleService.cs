namespace Probel.Geho.Services.BusinessLogic
{
    using System;
    using System.Collections.Generic;

    using Probel.Geho.Services.Dto;

    public interface IScheduleService
    {
        #region Methods

        void CreateWeek(DateTime dateInWeek);

        void FeedDay(DateTime date, IEnumerable<PersonDto> educators, GroupDto group, bool isMorning);

        void FeedDay(DateTime date, GroupDto group, bool isMorning);

        IEnumerable<ActivityDto> GetActivities();

        IEnumerable<PersonDto> GetEducatorsBusyInDay(DateTime currentDay, bool isMorning);

        IEnumerable<PersonDto> GetFreeBeneficiaries(GroupDto group, DateTime currentDay, bool isMorning);

        IEnumerable<PersonDto> GetFreeEducators(DateTime currentDay, bool isMorning);

        GroupDto GetGroup(int id);

        IEnumerable<GroupDto> GetGroups();

        IEnumerable<LunchTimeDto> GetLunchTimes();

        /// <summary>
        /// Gets all the mondays of the configured weeks
        /// </summary>
        /// <returns>A list of datetime representing the monday of the configured weeks</returns>
        IEnumerable<DateTime> GetMondays();

        WeekDto GetWeek(DateTime dateInWeek);

        WeekDto GetWeek(int id);

        IEnumerable<WeekDto> GetWeeks();

        bool WeekExists(DateTime date);

        #endregion Methods
    }
}