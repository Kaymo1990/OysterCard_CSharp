using NUnit.Framework;
using OysterCard;
using System;
using System.Collections.Specialized;
using System.Collections;
using System.Linq;

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
            oysterCard.TouchOut("Test1");
            Assert.AreEqual(5, oysterCard.oysterBalance);
        }

        [Test]
        public void JourneyHistory_HasKeyOfEntryStation_WhenTappedIn()
        {
            var expectedOutput = "Entry Station:";
            oysterCard.TouchIn("Test");
            Assert.AreEqual(expectedOutput, $"{oysterCard.journeyHistory().ElementAt(oysterCard.journeyHistory().Count - 1).Key}");
        }

        [Test]
        public void JourneyHistory_HasValueOfTest_WhenTappedIn()
        {
            var expectedOutput = "Test";
            oysterCard.TouchIn("Test");
            Assert.AreEqual(expectedOutput, $"{oysterCard.journeyHistory().ElementAt(oysterCard.journeyHistory().Count - 1).Value}");
        }

        [Test]
        public void InOysterJourney_IsTrue_WhenTouchedIn()
        {
            oysterCard.TouchIn("Test");
            Assert.IsTrue(oysterCard.journey.InJourney());
        }

        [Test]
        public void InOysterJourney_IsFalse_WhenTouchedOut()
        {
            oysterCard.TouchIn("Test");
            oysterCard.TouchOut("Test1");
            Assert.IsFalse(oysterCard.journey.InJourney());
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
            Assert.AreEqual("Test station", oysterCard.journeyHistory().ElementAt(oysterCard.journeyHistory().Count - 1).Value.ToString());
        }

        [Test]
        public void JourneyIncomplete_IsTrue_WhenTouchingInWhileIn()
        {
            var journey = new Journey();
            journey.UpdateJourneyEntry("Test");
            Assert.IsTrue(journey.JourneyIncomplete("In"));
        }

        [Test]
        public void JourneyIncomplete_IsTrue_WhenTouchingOutWhileOut()
        {
            var journey = new Journey();
            journey.UpdateJourneyExit("Test");
            Assert.IsTrue(journey.JourneyIncomplete("Out"));
        }

        [Test]
        public void OysterJourney_HasNoJourneys_ByDefault()
        {
            Assert.AreEqual(0, oysterCard.journeyHistory().Count);
        }

        [Test]
        public void OysterJourney_HasEntryStationAsKey_WhenTestPassedAsEntry()
        {
            var expectedOutput = "Entry Station: Test" + Environment.NewLine;
            oysterCard.TouchIn("Test");
            Assert.AreEqual(expectedOutput, oysterCard.ReturnFullJourney());
        }
        [Test]
        public void OysterJourney_HasTestAsValue_WhenTestPassedAsEntry()
        {
            var expectedOutput = "Exit Station: Test" + Environment.NewLine;
            oysterCard.TouchOut("Test");
            Assert.AreEqual(expectedOutput, oysterCard.ReturnFullJourney());
        }

        [Test]
        public void OysterJourney_HasTestAsEntryTest1AsExit_WhenCalled()
        {
            var expectedOutput = "Entry Station: Test" + Environment.NewLine + "Exit Station: Test1" + Environment.NewLine;
            oysterCard.TouchIn("Test");
            oysterCard.TouchOut("Test1");
            Assert.AreEqual(expectedOutput, oysterCard.ReturnFullJourney());
        }

        [Test]
        public void NewStation_ShouldHaveZoneOf3_WhenInstantiatedWith3()
        {
            var station = new Station("Test", 3);
            Assert.AreEqual(3, station.stationZone);
        }

        [Test]
        public void NewStation_ShouldHaveNameOfTest_WhenInstantiatedWithTest()
        {
            var station = new Station("Test", 3);
            Assert.AreEqual("Test", station.stationName);
        }
        [Test]
        public void JourneyIncompleteTappingIn_AppliesPenaltyCharge_OysterBalanceDeductedby6()
        {
            oysterCard.TouchIn("Test");
            oysterCard.TouchIn("Test2");
            Assert.AreEqual(4, oysterCard.oysterBalance);
        }

        public void JourneyIncompleteTappingOut_AppliesPenaltyCharge_OysterBalanceDeductedby6()
        {
            oysterCard.TouchIn("Test");
            oysterCard.TouchOut("Test2");
            oysterCard.TouchOut("Test3");
            Assert.AreEqual(3, oysterCard.oysterBalance);
        }
    }
}