namespace Probel.Geho.Shell.Core
{
    using System;

    public class CommandAttribute : Attribute
    {
        #region Constructors

        public CommandAttribute(string name, string shortName, string explanations, string pattern = null)
        {
            this.Name = name.ToLower();
            this.ShortName = shortName.ToLower();
            this.Description = explanations;
            this.Pattern = pattern ?? shortName;
        }

        #endregion Constructors

        #region Properties

        public string Description
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string Pattern
        {
            get; private set;
        }

        public string ShortName
        {
            get;
            private set;
        }

        #endregion Properties
    }
}