namespace Interview.Test
{
    using NUnit.Framework;

    using Probel.Geho.Services.Entities;
    using Probel.Geho.Services.Helpers;

    [TestFixture]
    public class Test_MomentDay
    {
        #region Methods

        [Test]
        public void When_Afternoon_And_Shoud_Be_Morning()
        {
            var md = MomentDay.Afternoon;
            Assert.That(md.IsMorning(), Is.False);
        }

        [Test]
        public void When_AllDay_And_Shoud_Be_Morning()
        {
            var md = MomentDay.AllDay;
            Assert.That(md.IsMorning(), Is.True);
        }

        [Test]
        public void When_Morning_And_Shoud_Be_Morning()
        {
            var md = MomentDay.Morning;

            Assert.That(md.IsMorning(), Is.True);
        }

        #endregion Methods
    }
}