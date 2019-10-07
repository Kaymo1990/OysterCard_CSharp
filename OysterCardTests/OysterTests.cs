using NUnit.Framework;
using OysterCard;

namespace Tests
{
    [TestFixture]
    public class OysterTests
    {
        Oyster oysterCard;
        [SetUp]
        public void Setup()
        {
            oysterCard = new Oyster(10.00);
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(10.00, oysterCard.oysterBalance);
        }
    }
}