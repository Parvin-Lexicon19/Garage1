using System;
using System.Collections.Generic;
using System.Text;

namespace Garage1
{
    class Car : Vehicle
    {
        internal Car(string fuelType, string CarNo, string color, int noOfWheels) : base(CarNo, color, noOfWheels)
        {
            FuelType = fuelType;
        }

        public string FuelType { get; set; }
    }
}
