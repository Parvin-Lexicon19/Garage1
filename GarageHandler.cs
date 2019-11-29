using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garage1
{
    class GarageHandler 
    {
        static Garage<Vehicle> garage;
        internal void CreateGarage(int capacity)
        {
            garage = new Garage<Vehicle>(capacity);
            garage.CallUI += UI.PrintWhenParked;

        }
        public Garage<Vehicle> Garage
        {
            get
            {
                return garage;
            }
        }
        internal Vehicle[] GetGarageVehicles() => garage.ToArray();
        //internal void GetGarageTypes()
        //{
        //    garage.GroupBy(v => v.GetType().Name).Select(v => new
        //    {
        //        TypeName = v.Key,
        //        Sum = v.Count()
        //    });
        //}
        internal bool RegNoExists(string input)
        {
            Vehicle vehicle = garage.FirstOrDefault(vehicle => string.Equals(vehicle.RegNo, input, StringComparison.CurrentCultureIgnoreCase));
            if (vehicle is null)
                return false;
            else
                return true;
        }
        internal List<Vehicle> SearchByProperties(string color, int noOfWhls) => garage.Where(v => string.Equals(v.Color, color, StringComparison.CurrentCultureIgnoreCase) && v.NoOfWheels == noOfWhls).ToList();
        internal Vehicle SearchByRegNo(string input) => garage.FirstOrDefault(vehicle => string.Equals(vehicle.RegNo, input, StringComparison.CurrentCultureIgnoreCase));
        internal Vehicle Unpark(string input)
        {
            var vehicle = garage.FirstOrDefault(vehicle => string.Equals(vehicle.RegNo, input, StringComparison.CurrentCultureIgnoreCase));
            //if (vehicle != null) garage.Remove(vehicle);

            int index = Array.FindIndex(garage.ToArray(), vehicle => vehicle.RegNo.Equals(input));
            if (index >= 0)
                garage.Remove(index);

            return vehicle;
        }

        internal bool ParkAirplane(int noOfEngines, string regNo, string color, int noOfWheels)
        {
            Airplane airplane = new Airplane(noOfEngines, regNo, color, noOfWheels);
            if (garage.Add(airplane))
                return true;
            else
                return false;
        }

       
        

        internal bool ParkMotorcycle(int cylinderVol, string regNo, string color, int noOfWheels)
        {
            Motorcycle motorcycle = new Motorcycle(cylinderVol, regNo, color, noOfWheels);
            if (garage.Add(motorcycle))
                return true;
            else
                return false;
        }
        internal bool ParkCar(string fuelType, string regNo, string color, int noOfWheels)
        {
            Car car = new Car(fuelType, regNo, color, noOfWheels);
            if (garage.Add(car))
                return true;
            else
                return false;
        }
        internal bool ParkBus(int noOfSeats, string regNo, string color, int noOfWheels)
        {
            Bus bus = new Bus(noOfSeats, regNo, color, noOfWheels);
            if (garage.Add(bus))
                return true;
            else
                return false;
        }
        internal bool ParkBoat(double length, string regNo, string color, int noOfWheels)
        {
            Boat boat = new Boat(length, regNo, color, noOfWheels);
            if (garage.Add(boat))
                return true;
            else
                return false;
        }
        internal bool IsFull() => garage.IsFull;        
    }
}
