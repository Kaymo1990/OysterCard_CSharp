using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OysterCard
{
    public class Journey
    {
        public IDictionary<string, string> journeyHistory = new Dictionary<string, string>();
        public int journeyID = 1;
        public bool InJourney()
        {
            if (journeyHistory.Count == 0)
            {
                return false;
            }
            if ($"{journeyHistory.ElementAt(journeyHistory.Count - 1).Key}" == $"Entry Station{journeyID}:")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateJourneyEntry(string entryJourneyStation)
        {
            if(InJourney() == true)
            {
                journeyID++;
            }
            journeyHistory.Add(new KeyValuePair<string, string>($"Entry Station{journeyID}:", entryJourneyStation));
        }

        public void UpdateJourneyExit(string exitJourneyStation)
        {
            journeyHistory.Add(new KeyValuePair<string, string>($"Exit Station{journeyID}:", exitJourneyStation));
            journeyID++;
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

        public bool JourneyIncomplete(string touchType)
        {
            if(touchType == "In")
            {
                if (InJourney() == true)
                {
                    return true;
                }
            }

            if (touchType == "Out")
            {
                if (InJourney() == false)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
