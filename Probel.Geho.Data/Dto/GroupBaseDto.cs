namespace Probel.Geho.Services.Dto
{
    using System.Diagnostics;

    [DebuggerDisplay("{Id} - {Name}")]
    public class GroupBaseDto : BaseDto
    {
        #region Properties

        public string Name
        {
            get;
            set;
        }

        #endregion Properties
    }
}