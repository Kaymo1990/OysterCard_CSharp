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
        private string entryStation = "";
        private string exitStation = "";
        public Oyster(double _balance)
        {
            balance = _balance;
        }

        public double oysterBalance { get => balance; set => balance = value; }
        public double maxOysterLimit { get => maxLimit; set => maxLimit = value; }
        public double minCharge { get => minimumCharge; set => minimumCharge = value; }
        public string entryJourneyStation { get => entryStation; set => entryStation = value; }
        public string exitJourneyStation { get => exitStation; set => exitStation = value; }

        public IDictionary<string, string> journeyHistory = new Dictionary<string, string>();

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
            entryJourneyStation = entryStation;
            UpdateJourneyEntry();
        }

        public void TouchOut(string exitStation)
        {
            Deduct(minCharge);
            entryJourneyStation = "";
            exitJourneyStation = exitStation;
            UpdateJourneyExit();
        }

        public bool InJourney()
        {
            if (entryJourneyStation == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void UpdateJourneyEntry()
        {
            journeyHistory.Add(new KeyValuePair<string, string>("Entry Station:", entryJourneyStation));
        }

        public void UpdateJourneyExit()
        {
            journeyHistory.Add(new KeyValuePair<string, string>("Exit Station:", exitJourneyStation));
        }

        public string ReturnFullJourney()
        {
            var fullJourney = "";
            foreach (KeyValuePair<string, string> journey in journeyHistory)
            {
                fullJourney += $"{ journey.Key } { journey.Value }{Environment.NewLine}";
            }
            return fullJourney;
        }

        private double Deduct(double amount)
        {
            oysterBalance -= amount;
            return oysterBalance;
        }
    }

}
