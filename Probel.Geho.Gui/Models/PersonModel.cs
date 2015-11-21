namespace Probel.Geho.Gui.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Data.Dto;
    using Probel.Mvvm.DataBinding;

    public class PersonModel : ObservableObject
    {
        #region Fields

        private int id;
        private bool isEducator;
        private bool isSelected;
        private string name;
        private string surname;

        #endregion Fields

        #region Constructors

        public PersonModel(PersonDto dto)
        {
            this.Id = dto.Id;
            this.IsEducator = dto.IsEducator;
            this.Name = dto.Name;
            this.Surname = dto.Surname;
            this.Person = dto;
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

        public static IEnumerable<PersonModel> ToModel(IEnumerable<PersonDto> dtoCollection)
        {
            var list = new List<PersonModel>();
            foreach (var dto in dtoCollection)
            {
                list.Add(new PersonModel(dto));
            }
            return list;
        }

        #endregion Methods
    }
}