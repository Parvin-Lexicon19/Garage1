using System;
using System.Collections.Generic;
using System.Text;

namespace Garage1
{
    class Motorcycle : Vehicle
    {
        internal Motorcycle(int cylinderVol, string MotorcycleNo, string color, int noOfWheels) : base(MotorcycleNo, color, noOfWheels)
        {
            CylinderVol = cylinderVol;
        }

        public int CylinderVol { get; set; }
    }
}
