namespace Probel.Geho.Data.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    public class BusinessRuleException : Exception
    {
        #region Constructors

        public BusinessRuleException()
            : this("Validation rule error.")
        {
        }

        public BusinessRuleException(string message)
            : base(message)
        {
        }

        public BusinessRuleException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected BusinessRuleException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        #endregion Constructors
    }
}