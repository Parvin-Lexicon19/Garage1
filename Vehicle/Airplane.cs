using System;
using System.Collections.Generic;
using System.Text;

namespace Garage1
{
    class Airplane : Vehicle
    {
        internal Airplane(int noOfEngines, string AirplaneNo, string color, int noOfWheels) : base(AirplaneNo, color, noOfWheels)
        {
            NoOfEngines = noOfEngines;
        }

        public int NoOfEngines { get; set; }
    }
}
