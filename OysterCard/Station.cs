using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OysterCard
{
    public class Station
    {
        public Station(string name, int zone)
        {
            stationName = name;
            stationZone = zone;

        }

        public string stationName { get; }
        public int stationZone { get; }
    }
}
