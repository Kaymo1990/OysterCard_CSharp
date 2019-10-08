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
            oysterCard.oysterBalance = 6.0;
            oysterCard.TouchOut();
            Assert.AreEqual(5, oysterCard.oysterBalance);
        }

        [Test]
        public void InOysterJourney_IsTrue_WhenTouchedIn()
        {
            oysterCard.TouchIn("Test");
            Assert.IsTrue(oysterCard.InJourney());
        }

        [Test]
        public void InOysterJourney_IsFalse_WhenTouchedOut()
        {
            oysterCard.TouchIn("Test");
            oysterCard.TouchOut();
            Assert.IsFalse(oysterCard.InJourney());
        }

        [Test]
        public void TouchIn_ThrowsException_WhenBalanceBelow1()
        {
            oysterCard.oysterBalance = 0.5;
            Assert.Throws<Exception>(() => oysterCard.TouchIn("Test"));
        }

        [Test]
        public void TouchIn_UpdatesEntryStation_WhenTouchIn()
        {
            oysterCard.TouchIn("Test station");
            Assert.AreEqual("Test station", oysterCard.entryJourneyStation);
        }
    }
}