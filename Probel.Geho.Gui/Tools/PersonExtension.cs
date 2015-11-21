namespace Probel.Geho.Gui.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Gui.Models;

    public static class PersonExtension
    {
        #region Methods

        public static void SetSelected(this IEnumerable<PersonModel> people, bool isSelected = true)
        {
            foreach (var person in people)
            {
                person.IsSelected = isSelected;
            }
        }

        #endregion Methods
    }
}