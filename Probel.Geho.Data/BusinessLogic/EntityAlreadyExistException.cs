namespace Probel.Geho.Data.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    public class EntityAlreadyExistException : Exception
    {
        #region Constructors

        public EntityAlreadyExistException()
            : this("The item already exists in the database")
        {
        }

        public EntityAlreadyExistException(string message)
            : base(message)
        {
        }

        public EntityAlreadyExistException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected EntityAlreadyExistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion Constructors
    }
}