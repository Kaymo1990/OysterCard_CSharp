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
        public bool InJourney(JourneyLog journeyLog)
        {
            if (journeyLog.journeyHistory.Count == 0)
            {
                return false;
            }
            if ($"{journeyLog.journeyHistory.ElementAt(journeyLog.journeyHistory.Count - 1).Key}" == $"Entry Station{journeyLog.journeyID}:")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool JourneyIncomplete(string touchType, JourneyLog journeyLog)
        {
            if(touchType == "In")
            {
                if (InJourney(journeyLog) == true)
                {
                    return true;
                }
            }

            if (touchType == "Out")
            {
                if (InJourney(journeyLog) == false)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
