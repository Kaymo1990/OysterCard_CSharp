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
        public void OysterBalance_ReturnedBalance_Is10()
        {
            Assert.AreEqual(10.00, oysterCard.oysterBalance);
        }

        [Test]
        public void OysterTopUp_ReturnedBalance_is20WhenPassed10()
        {
            Assert.AreEqual(20.00, oysterCard.TopUp(10.00));
        }
    }
}