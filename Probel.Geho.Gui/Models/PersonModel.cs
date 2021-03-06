﻿namespace Probel.Geho.Gui.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Mvvm.Toolkit.DataBinding;

    using Probel.Geho.Services.Dto;

    public class PersonModel : ObservableObject
    {
        #region Fields

        private int id;
        private bool isEducator;
        private bool isSelected;
        private bool isTrainee;
        private string name;
        private string surname;

        #endregion Fields

        #region Constructors

        public PersonModel(PersonDto dto, bool isSelected = false)
        {
            this.Id = dto.Id;
            this.IsEducator = dto.IsEducator;
            this.IsSelected = false;
            this.Name = dto.Name;
            this.Surname = dto.Surname;
            this.Person = dto;
            this.IsTrainee = dto.IsTrainee;
        }

        #endregion Constructors

        #region Properties

        public int Id
        {
            get { return this.id; }
            set
            {
                this.id = value;
                this.OnPropertyChanged(() => Id);
            }
        }

        public bool IsEducator
        {
            get { return this.isEducator; }
            set
            {
                this.isEducator = value;
                this.OnPropertyChanged(() => IsEducator);
            }
        }

        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                this.isSelected = value;
                this.OnPropertyChanged(() => IsSelected);
            }
        }

        public bool IsTrainee
        {
            get { return this.isTrainee; }
            set
            {
                this.isTrainee = value;
                this.OnPropertyChanged(() => IsTrainee);
            }
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.OnPropertyChanged(() => Name);
            }
        }

        public PersonDto Person
        {
            get;
            private set;
        }

        public string Surname
        {
            get { return this.surname; }
            set
            {
                this.surname = value;
                this.OnPropertyChanged(() => Surname);
            }
        }

        #endregion Properties

        #region Methods

        public static IEnumerable<PersonDto> ToDto(IEnumerable<PersonModel> mCollection)
        {
            var list = new List<PersonDto>();
            foreach (var model in mCollection)
            {
                list.Add(model.Person);
            }
            return list;
        }

        public static IEnumerable<PersonModel> ToModel(IEnumerable<PersonDto> dtoCollection, bool isSelected = false)
        {
            var list = new List<PersonModel>();
            foreach (var dto in dtoCollection.OrderBy(e => e.Name).ThenBy(e => e.Surname))
            {
                list.Add(new PersonModel(dto, isSelected));
            }
            return list;
        }

        #endregion Methods
    }

    public static class PersonModelExtension
    {
        #region Methods

        public static IEnumerable<PersonDto> ToDto(this IEnumerable<PersonModel> models)
        {
            var result = new List<PersonDto>();
            foreach (var model in models)
            {
                result.Add(model.Person);
            }
            return result;
        }

        public static IEnumerable<PersonModel> ToModel(this IEnumerable<PersonDto> dto, bool isSelected = false)
        {
            return PersonModel.ToModel(dto);
        }

        #endregion Methods
    }
}