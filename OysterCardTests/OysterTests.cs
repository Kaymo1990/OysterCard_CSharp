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
        Station entryStation;
        Station exitStation;
        Oyster oysterCard;
        [SetUp]
        public void Setup()
        {
            oysterCard = new Oyster(10.00);
            entryStation = new Station("Test", 1);
            exitStation = new Station("Test1", 1);
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
        public void OysterDeduct_Deducts1GBP_WhenTappingOut()
        {
            
            oysterCard.TouchIn(entryStation);
            oysterCard.TouchOut(exitStation);
            Assert.AreEqual(9, oysterCard.oysterBalance);
        }

        [Test]
        public void JourneyHistory_HasKeyOfEntryStation_WhenTappedIn()
        {
            var expectedOutput = "Entry Station1:";
            oysterCard.TouchIn(entryStation);
            Assert.AreEqual(expectedOutput, $"{oysterCard.journeyLog.journeyHistory.ElementAt(oysterCard.journeyLog.journeyHistory.Count - 1).Key}");
        }

        [Test]
        public void JourneyHistory_HasValueOfTest_WhenTappedIn()
        {
            var expectedOutput = "Test";
            oysterCard.TouchIn(entryStation);
            Assert.AreEqual(expectedOutput, $"{oysterCard.journeyLog.journeyHistory.ElementAt(oysterCard.journeyLog.journeyHistory.Count - 1).Value.stationName}");
        }

        [Test]
        public void InOysterJourney_IsTrue_WhenTouchedIn()
        {
            oysterCard.TouchIn(entryStation);
            Assert.IsTrue(oysterCard.journeyLog.InJourney());
        }

        [Test]
        public void InOysterJourney_IsFalse_WhenTouchedOut()
        {
            oysterCard.TouchIn(entryStation);
            oysterCard.TouchOut(exitStation);
            Assert.IsFalse(oysterCard.journeyLog.InJourney());
        }

        [Test]
        public void TouchIn_ThrowsException_WhenBalanceBelow1()
        {
            oysterCard.oysterBalance = 0.5;
            Assert.Throws<Exception>(() => oysterCard.TouchIn(entryStation));
        }

        [Test]
        public void TouchIn_UpdatesEntryStation_WhenTouchIn()
        {
            oysterCard.TouchIn(entryStation);
            Assert.AreEqual("Test", oysterCard.journeyLog.journeyHistory.ElementAt(oysterCard.journeyLog.journeyHistory.Count - 1).Value.stationName);
        }

        [Test]
        public void JourneyIncomplete_IsTrue_WhenTouchingInWhileIn()
        {
            var journeyLog = new JourneyLog(new Journey());
            journeyLog.StartEntryJourney(entryStation);
            Assert.IsTrue(journeyLog.JourneyIncomplete("In"));
        }

        [Test]
        public void JourneyIncomplete_IsTrue_WhenTouchingOutWhileOut()
        {
            var journeyLog = new JourneyLog(new Journey());
            journeyLog.FinishExitJourney(exitStation);
            Assert.IsTrue(journeyLog.JourneyIncomplete("Out"));
        }

        [Test]
        public void OysterJourney_HasNoJourneys_ByDefault()
        {
            Assert.AreEqual(0, oysterCard.journeyLog.journeyHistory.Count);
        }

        [Test]
        public void OysterJourney_HasEntryStationAsKey_WhenTestPassedAsEntry()
        {
            var expectedOutput = "Entry Station1: Test" + Environment.NewLine;
            oysterCard.TouchIn(entryStation);
            Assert.AreEqual(expectedOutput, oysterCard.ReturnFullJourney());
        }
        [Test]
        public void OysterJourney_HasTestAsValue_WhenTestPassedAsEntry()
        {
            var expectedOutput = "Exit Station1: Test" + Environment.NewLine;
            oysterCard.TouchOut(entryStation);
            Assert.AreEqual(expectedOutput, oysterCard.ReturnFullJourney());
        }

        [Test]
        public void OysterJourney_HasTestAsEntryTest1AsExit_WhenCalled()
        {
            var expectedOutput = "Entry Station1: Test" + Environment.NewLine + "Exit Station1: Test1" + Environment.NewLine;
            oysterCard.TouchIn(entryStation);
            oysterCard.TouchOut(exitStation);
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
            oysterCard.TouchIn(entryStation);
            oysterCard.TouchIn(exitStation);
            Assert.AreEqual(4, oysterCard.oysterBalance);
        }

        [Test]
        public void JourneyIncompleteTappingOut_AppliesPenaltyCharge_OysterBalanceDeductedby6()
        {
            oysterCard.TouchIn(entryStation);
            oysterCard.TouchOut(exitStation);
            oysterCard.TouchOut(exitStation);
            Assert.AreEqual(3, oysterCard.oysterBalance);
        }

        [Test]
        public void JourneyIncomplete_UpdatesEntryStationIDto2IfIn_EntryStationIDIs2()
        {
            oysterCard.TouchIn(entryStation);
            oysterCard.TouchIn(entryStation);

            var expectedOutput = "Entry Station2:";
            Assert.AreEqual(expectedOutput, oysterCard.journeyLog.journeyHistory.ElementAt(oysterCard.journeyLog.journeyHistory.Count - 1).Key.ToString());
        }

        [Test]
        public void OysterFare_Returns3_WhenPassedZone1And3()
        {
            var station1 = new Station("Test", 1);
            var station2 = new Station("Test1", 3);
            oysterCard.TouchIn(station1);
            oysterCard.TouchOut(station2);
            Assert.AreEqual(3.00, oysterCard.ExposeCalcFare());
        }
    }
}