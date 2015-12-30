namespace Probel.Geho.Services.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    [Serializable]
    public class EntityNotFountException : Exception
    {
        #region Constructors

        public EntityNotFountException()
            : this("The entity you searched does not exist in the database")
        {
        }

        public EntityNotFountException(string message)
            : base(message)
        {
        }

        public EntityNotFountException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected EntityNotFountException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion Constructors
    }
}