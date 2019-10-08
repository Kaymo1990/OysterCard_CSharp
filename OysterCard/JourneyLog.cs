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
        public IDictionary<string, Station> journeyHistory = new Dictionary<string, Station>();

        public Journey journey { get; }

        public void StartEntryJourney(Station entryJourneyStation)
        {
            if(InJourney() == true)
            {
                journeyID++;
            }
            journeyHistory.Add(new KeyValuePair<string, Station>($"Entry Station{journeyID}:", entryJourneyStation));
        }

        public void FinishExitJourney(Station exitJourneyStation)
        {
            journeyHistory.Add(new KeyValuePair<string, Station>($"Exit Station{journeyID}:", exitJourneyStation));
            journeyID++;
        }

        public string ReturnFullJourney()
        {
            var fullJourney = "";
            foreach (KeyValuePair<string, Station> journey in journeyHistory)
            {
                fullJourney += $"{ journey.Key } { journey.Value.stationName }{Environment.NewLine}";
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
