namespace Probel.Geho.Services.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Entity
    {
        #region Properties

        [Key]
        public int Id
        {
            get; set;
        }

        #endregion Properties
    }
}