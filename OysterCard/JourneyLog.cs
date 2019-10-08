using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OysterCard
{
    public class JourneyLog
    {
        public JourneyLog(Journey _journey)
        {
            journey = _journey;
        }

        public int journeyID = 1;
        public IDictionary<string, string> journeyHistory = new Dictionary<string, string>();

        public Journey journey { get; }

        public void StartEntryJourney(string entryJourneyStation)
        {
            if(InJourney() == true)
            {
                journeyID++;
            }
            journeyHistory.Add(new KeyValuePair<string, string>($"Entry Station{journeyID}:", entryJourneyStation));
        }

        public void FinishExitJourney(string exitJourneyStation)
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
            return journey.JourneyIncomplete(touchType, this);
        }

        public bool InJourney()
        {
            return journey.InJourney(this);
        }
    }
}
