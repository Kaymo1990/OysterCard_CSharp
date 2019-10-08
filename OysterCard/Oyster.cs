using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OysterCard
{
    public class Oyster
    {
        private double balance;
        private double maxLimit = 90.00;
        private double minimumCharge = 1.00;
        private double penaltyCharge = 6.00;
        public Oyster(double _balance)
        {
            balance = _balance;
        }

        public double oysterBalance { get => balance; set => balance = value; }
        public double maxOysterLimit { get => maxLimit; set => maxLimit = value; }
        public double minCharge { get => minimumCharge; set => minimumCharge = value; }
        public double penaltyFare { get => penaltyCharge; set => penaltyCharge = value; }

        public Journey journey = new Journey();

        public double TopUp(double amount)
        {
            if ((oysterBalance+amount) > maxOysterLimit)
            {
                throw new Exception($"The maximum balance is {maxOysterLimit} GBP");
            }
                oysterBalance += amount;
                return oysterBalance;
        }


        public void TouchIn(string entryStation)
        {
            if (oysterBalance < 1.00)
            {
                throw new Exception("You don't have the minimum balance of 1.00 GBP");
            }


            if (journey.JourneyIncomplete("In"))
            {
                Deduct(penaltyFare);
            }

            journey.UpdateJourneyEntry(entryStation);
        }

        public void TouchOut(string exitStation)
        {
            if (journey.JourneyIncomplete("Out"))
            {
                Deduct(penaltyFare);
            }

            else
            {
                Deduct(minCharge);
            }
            journey.UpdateJourneyExit(exitStation);
        }

        public string ReturnFullJourney()
        {
            return journey.ReturnFullJourney();
        }

        public IDictionary<string, string> JourneyHistory()
        {
            return journey.journeyHistory;
        }



        private double Deduct(double amount)
        {
            oysterBalance -= amount;
            return oysterBalance;
        }
    }

}
