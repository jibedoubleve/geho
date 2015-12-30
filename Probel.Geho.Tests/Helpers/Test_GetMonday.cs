namespace Probel.Geho.Tests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using Probel.Geho.Services.Helpers;

    [TestFixture]
    public class Test_GetMonday
    {
        #region Fields

        private readonly DateTime Monday = new DateTime(2015, 11, 30);

        #endregion Fields

        #region Methods

        [Test]
        public void When_Day_Is_1Monday()
        {
            var dt = new DateTime(2015, 11, 30);
            var date = dt.GetMonday();

            Assert.That(date, Is.EqualTo(Monday));
        }

        [Test]
        public void When_Day_Is_2Tuesday()
        {
            var dt = new DateTime(2015, 12, 1);
            var date = dt.GetMonday();

            Assert.That(date, Is.EqualTo(Monday));
        }

        [Test]
        public void When_Day_Is_3Wednesday()
        {
            var dt = new DateTime(2015, 12, 2);
            var date = dt.GetMonday();

            Assert.That(date, Is.EqualTo(Monday));
        }

        [Test]
        public void When_Day_Is_4Thursday()
        {
            var dt = new DateTime(2015, 12, 3);
            var date = dt.GetMonday();

            Assert.That(date, Is.EqualTo(Monday));
        }

        [Test]
        public void When_Day_Is_5Friday()
        {
            var dt = new DateTime(2015, 12, 4);
            var date = dt.GetMonday();

            Assert.That(date, Is.EqualTo(Monday));
        }

        [Test]
        public void When_Day_Is_6Saterday()
        {
            var dt = new DateTime(2015, 12, 5);
            var date = dt.GetMonday();

            Assert.That(date, Is.EqualTo(Monday));
        }

        [Test]
        public void When_Day_Is_7Sunday()
        {
            var dt = new DateTime(2015, 12, 6);
            var date = dt.GetMonday();

            Assert.That(date, Is.EqualTo(Monday));
        }

        #endregion Methods
    }
}