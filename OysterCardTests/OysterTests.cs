using NUnit.Framework;
using OysterCard;
using System;

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
        [Test]
        public void MaxOysterLimit_Returns90_WhenCalled()
        {
            Assert.AreEqual(90.00, oysterCard.maxOysterLimit);
        }

        [Test]
        public void OysterTopUp_ThrowsException_WhenExceeding90GBPBalance()
        {
            Assert.Throws<Exception>(() => oysterCard.TopUp(85.00));
        }

        [Test]
        public void OysterDeduct_Deducts5GBP_WhenPassed5()
        {
            Assert.AreEqual(5, oysterCard.Deduct(5.00));
        }

        [Test]
        public void InOysterJourney_IsTrue_WhenTouchedIn()
        {
            Assert.IsTrue(oysterCard.TouchIn());
        }

        [Test]
        public void InOysterJourney_IsFalse_WhenTouchedOut()
        {
            oysterCard.TouchIn();
            Assert.IsFalse(oysterCard.TouchOut());
        }

        [Test]
        public void TouchIn_ThrowsException_WhenBalanceBelow1()
        {
            oysterCard.Deduct(9.50);
            Assert.Throws<Exception>(() => oysterCard.TouchIn());
        }
    }
}