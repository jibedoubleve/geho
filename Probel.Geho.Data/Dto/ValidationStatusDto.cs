namespace Probel.Geho.Data.Dto
{
    public class ValidationStatusDto
    {
        #region Constructors

        public ValidationStatusDto(bool isValid, string error = "")
        {
            this.IsValid = isValid;
            this.Error = error;
        }

        #endregion Constructors

        #region Properties

        public string Error
        {
            get; private set;
        }

        public bool IsValid
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        internal static ValidationStatusDto Invalid(string error)
        {
            return new ValidationStatusDto(false, error);
        }

        internal static ValidationStatusDto Valid()
        {
            return new ValidationStatusDto(true);
        }

        #endregion Methods
    }
}