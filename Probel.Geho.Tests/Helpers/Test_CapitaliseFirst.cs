namespace Interview.Test
{
    using NUnit.Framework;

    using Probel.Geho.Data.Helpers;

    [TestFixture]
    public class Test_CapitaliseFirst
    {
        #region Methods

        [Test]
        public void When_ALL_Is_Capitalised()
        {
            var jean = "JEAN";
            var result = jean.CapitaliseFirst();
            Assert.That(result, Is.EqualTo("Jean"));
        }

        [Test]
        public void When_ALL_Is_LowerCase()
        {
            var jean = "jean";
            var result = jean.CapitaliseFirst();
            Assert.That(result, Is.EqualTo("Jean"));
        }

        [Test]
        public void When_First_Letter_Is_Capitalised()
        {
            var jean = "Jean";
            var result = jean.CapitaliseFirst();
            Assert.That(result, Is.EqualTo("Jean"));
        }

        #endregion Methods
    }
}