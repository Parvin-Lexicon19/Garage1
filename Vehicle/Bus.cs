using System;
using System.Collections.Generic;
using System.Text;

namespace Garage1
{
    class Bus : Vehicle
    {
        internal Bus(int noOfSeats, string BusNo, string color, int noOfWheels) : base(BusNo, color, noOfWheels)
        {
            NoOfSeats = noOfSeats;
        }

        public int NoOfSeats { get; set; }
    }
}
