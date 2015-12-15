namespace Probel.Geho.Shell.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Probel.Geho.Shell.Core;

    public class ArgumentProcessor
    {
        #region Fields

        private readonly IEnumerable<Argument> Args;

        #endregion Fields

        #region Constructors

        public ArgumentProcessor(IEnumerable<Argument> args)
        {
            this.Args = args;
        }

        #endregion Constructors

        #region Properties

        public int Count
        {
            get { return this.Args.Count(); }
        }

        public bool HasArgs
        {
            get { return this.Args.Count() > 0; }
        }

        #endregion Properties

        #region Methods

        public string Get(string name)
        {
            name = name.ToLower().Replace("-", string.Empty);
            var value = (from a in Args
                         where a.Command.ToLower() == name
                         select a.Args).FirstOrDefault();

            if (string.IsNullOrEmpty(value)) { throw new ArgumentException("Expected argument not found.", name); }
            else { return value.Trim(); }
        }

        public bool Has(string name)
        {
            name = name.ToLower().Replace("-", string.Empty);
            var value = (from a in Args
                         where a.Command.ToLower() == name
                         select a.Args).FirstOrDefault();
            return (value != null);
        }

        public bool ParseBool(string isShortStr)
        {
            bool boolean = false;
            if (!Boolean.TryParse(isShortStr, out boolean)) { throw new ArgumentException("The boolean cannot be parsed", "isShortStr"); }
            return boolean;
        }

        public DateTime ParseDate(string strDate)
        {
            DateTime date;
            if (!DateTime.TryParse(strDate, out date)) { throw new ArgumentException("The date cannot be parsed", "strDate"); }
            return date;
        }

        public DayOfWeek ParseDayOfWeek(string dowStr)
        {
            DayOfWeek result ;
            if (!Enum.TryParse(dowStr, out result)) { throw new ArgumentException("The 'DayOfWeek' cannot be parsed", "dowStr"); }
            return result;
        }

        public int ParseId(string strId)
        {
            var id = 0;
            if (!Int32.TryParse(strId, out id)) { throw new ArgumentException("The id cannot be parsed", "strId"); }
            return id;
        }

        public IEnumerable<int> ParseIdCollection(string[] idList)
        {
            var resultList = new List<int>();

            foreach (var id in idList)
            {
                var result = 0;
                if (Int32.TryParse(id, out result)) { resultList.Add(result); }
                else { throw new ArgumentException(string.Format("Impossible to parse the id with value {0}", id), "idList"); }
            }
            return resultList;
        }

        #endregion Methods
    }
}