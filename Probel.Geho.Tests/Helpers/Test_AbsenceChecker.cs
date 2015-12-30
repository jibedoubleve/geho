namespace Probel.Geho.Tests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using Probel.Geho.Services.Business_Logic.Schedule;
    using Probel.Geho.Services.Entities;

    public class Test_AbsenceChecker
    {
        #region Methods

        [Test]
        public void When_Is_Absent()
        {
            var absence = new Absence()
            {
                Start = new DateTime(2015, 10, 1),
                End = new DateTime(2015, 10, 30),
            };
            var date = new DateTime(2015, 10, 11);
            var ac = new AbsenceChecker(absence, date);

            Assert.That(ac.IsAbsent, Is.True);
        }

        [Test]
        public void When_Is_Absent_And_End_Is_Date()
        {
            var absence = new Absence()
            {
                Start = new DateTime(2015, 10, 1),
                End = new DateTime(2015, 10, 30),
            };
            var date = new DateTime(2015, 10, 30);
            var ac = new AbsenceChecker(absence, date);

            Assert.That(ac.IsAbsent, Is.True);
        }

        [Test]
        public void When_Is_Absent_And_Start_Is_Date()
        {
            var absence = new Absence()
            {
                Start = new DateTime(2015, 10, 1),
                End = new DateTime(2015, 10, 30),
            };
            var date = new DateTime(2015, 10, 1);
            var ac = new AbsenceChecker(absence, date);

            Assert.That(ac.IsAbsent, Is.True);
        }

        [Test]
        public void When_Is_NOT_Absent()
        {
            var absence = new Absence()
            {
                Start = new DateTime(2015, 10, 1),
                End = new DateTime(2015, 10, 30),
            };
            var date = new DateTime(2015, 12, 11);
            var ac = new AbsenceChecker(absence, date);

            Assert.That(ac.IsAbsent, Is.False);
        }

        #endregion Methods
    }
}