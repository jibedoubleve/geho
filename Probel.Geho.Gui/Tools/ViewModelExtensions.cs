namespace Probel.Geho.Gui.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Probel.Geho.Gui.ViewModels.Controls;
    using Probel.Geho.Services.BusinessLogic;
    using Probel.Geho.Services.Dto;

    using ViewModels;

    public static class ViewModelExtensions
    {
        #region Methods

        public static IEnumerable<ActivityCardViewModel> ToViewModels(this IEnumerable<ActivityDto> activities)
        {
            var list = new List<ActivityCardViewModel>();
            foreach (var a in activities)
            {
                list.Add(new ActivityCardViewModel(a));
            }
            return list;
        }

        public static IEnumerable<ActivityViewModel> ToViewModels(this IEnumerable<ActivityDto> activities, IHrService service, ILoadeableViewModel parentVm)
        {
            var list = new List<ActivityViewModel>();
            foreach (var a in activities)
            {
                list.Add(new ActivityViewModel(a, service, parentVm));
            }
            return list;
        }

        public static IEnumerable<ManageGroupDayViewModel> ToViewModels(this IEnumerable<GroupDto> groups, IScheduleService srv, ManageDayViewModel parentVm)
        {
            if (srv == null) { throw new ArgumentNullException("srv"); }
            var list = new List<ManageGroupDayViewModel>();
            foreach (var item in groups)
            {
                var vm = new ManageGroupDayViewModel(item, srv, parentVm);
                list.Add(vm);
            }
            return list;
        }

        public static IEnumerable<GroupViewModel> ToViewModels(this IEnumerable<GroupDto> groups, IHrService srv, ILoadeableViewModel parentVm)
        {
            var list = new List<GroupViewModel>();
            foreach (var item in groups.OrderBy(e => e.Order))
            {
                list.Add(new GroupViewModel(srv, parentVm) { Group = item, });
            }
            return list;
        }

        public static IEnumerable<AbsenceViewModel> ToViewModels(this IEnumerable<AbsenceDto> absences, IHrService service, ILoadeableViewModel vm)
        {
            var result = new List<AbsenceViewModel>();
            foreach (var absence in absences)
            {
                result.Add(new AbsenceViewModel(service, vm) { Absence = absence });
            }
            return result;
        }

        public static IEnumerable<PersonFlatBusyViewModel> ToViewModels(this IEnumerable<PersonDto> people, IScheduleService service, ManageGroupDayViewModel parentVm)
        {
            var list = new List<PersonFlatBusyViewModel>();
            foreach (var person in people.OrderBy(e => e.Name).ThenBy(e => e.Surname))
            {
                list.Add(new PersonFlatBusyViewModel(person, service, parentVm));
            }
            return list;
        }

        public static IEnumerable<PersonViewModel> ToViewModels(this IEnumerable<PersonDto> absences, IHrService service, ILoadeableViewModel vm)
        {
            var result = new List<PersonViewModel>();
            foreach (var person in absences)
            {
                result.Add(new PersonViewModel(vm, service) { Person = person, });
            }
            return result;
        }

        #endregion Methods
    }
}