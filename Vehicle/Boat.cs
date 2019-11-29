using System;
using System.Collections.Generic;
using System.Text;

namespace Garage1
{
    class Boat : Vehicle
    {
        internal Boat(double length, string BoatNo, string color, int noOfWheels) : base(BoatNo, color, noOfWheels)
        {
            Length = length;
        }

        public double Length { get; set; }
    }
}
