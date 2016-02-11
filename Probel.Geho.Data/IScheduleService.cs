namespace Probel.Geho.Services.BusinessLogic
{
    using System;
    using System.Collections.Generic;

    using Probel.Geho.Services.Dto;

    public interface IScheduleService
    {
        #region Methods

        void CreateWeek(DateTime dateInWeek);

        void FeedDay(DateTime date, IEnumerable<PersonDto> educators, GroupBaseDto group, bool isMorning);

        void FeedDay(DateTime date, GroupBaseDto group, bool isMorning);

        IEnumerable<ActivityDto> GetActivities(bool includeDeactivated = false);

        IEnumerable<GroupBaseDto> GetBaseGroups();

        IEnumerable<PersonDto> GetEducatorsBusyInDay(DateTime currentDay, bool isMorning);

        IEnumerable<PersonDto> GetFreeBeneficiaries(GroupBaseDto group, DateTime currentDay, bool isMorning);

        IEnumerable<PersonDto> GetFreeEducators(DateTime currentDay, bool isMorning);

        GroupDto GetGroup(int id);

        IEnumerable<GroupDto> GetGroups();

        IEnumerable<LunchTimeDto> GetLunchTimes();

        /// <summary>
        /// Get all the weeks (mondays) that are not configured in the schedule manager
        /// </summary>
        /// <param name="weeks">number of unconfigured weeks to return. By default 6 months</param>
        /// <param name="from">Starting date. It'll be converted to the monday of the week that contains this date</param>
        /// <returns>A list of not configured mondays</returns>
        IEnumerable<DateTime> GetNotConfiguredMondays(DateTime from,int weeks = 26);

        WeekDto GetWeek(DateTime dateInWeek);

        WeekDto GetWeek(int id);

        IEnumerable<DateTime> GetWeekDates(bool isPastExcluded = false);

        IEnumerable<WeekDto> GetWeeks();

        bool WeekExists(DateTime date);

        #endregion Methods
    }
}