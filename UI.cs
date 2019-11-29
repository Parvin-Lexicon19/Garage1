using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Garage1
{
    internal static class UI
    {
        static GarageHandler garageHandler = new GarageHandler();
        static BinaryFormatter formatter;
        internal static void FirstMenu()
        {
            while (true)
            {
                int firstMenuInput = Util.AskForInt("\n1. Create a new garage"
                                         + "\n0. Exit the application");
                if (firstMenuInput == 0) break;

                switch (firstMenuInput)
                {
                    case 1:
                        CreateGarage();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Please enter some valid input (0, 1)");
                        break;
                }
            }
        }

        private static void CreateGarage()
        {
            while (true)
            {
                int input = Util.AskForInt("\n-Please enter garage max. capacity:");
                if (input > 0)
                {
                    garageHandler.CreateGarage(input);
                    Console.WriteLine($"A garage with {input} capacity created!\n");

                    SecondMenu();
                }
                else
                    Console.WriteLine("Please enter a number greater than 0");
            }
        }
        private static void SecondMenu()
        {
            
            while (true)
            {
                int secondMenuInput = Util.AskForInt("\n1. Park a vehicle"
                                                   + "\n2. List of parked vehicles"
                                                   + "\n3. List of vehicle types and how many of each"
                                                   + "\n4. Search for a vehicle based on properties"
                                                   + "\n5. Search for a vehicle based on Registration No."
                                                   + "\n6. Unpark a vehicle"
                                                   + "\n0. Exit the application");
                
                switch (secondMenuInput)
                {
                    case 1:
                        Park();
                        break;
                    case 2:
                        ParkedVehiclesList();
                        break;
                    case 3:
                        ParkedVehiclesTypeList();
                        break;
                    case 4:
                        SearchByProperties();
                        break;
                    case 5:
                        SearchByRegNo();
                        break;
                    case 6:
                        Unpark();
                        break;
                    case 0:
                        //Save();
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("Please enter some valid input (0, 1, 2, 3, 4, 5, 6)");
                        break;
                }
            }
        }
        private static void Park()
        {
            while (true)
            {
                int vehicleIndxInput = Util.AskForInt("Please select vehicle type you want to park: \n"
                                                   + "\n1. Airplane"
                                                   + "\n2. Motorcycle"
                                                   + "\n3. Car"
                                                   + "\n4. Bus"
                                                   + "\n5. Boat"
                                                   + "\n0. Quit");
                if (vehicleIndxInput == 0) break;
                else if (vehicleIndxInput > 0 && vehicleIndxInput < 6)
                    if (!garageHandler.IsFull())
                        AskVehicleInfo(vehicleIndxInput);
                    else
                    {
                        Console.WriteLine("Sorry! garage is full..........\n");
                        return;
                    }
                else
                    Console.WriteLine("Please enter some valid input (0, 1, 2, 3, 4, 5)");
            }
        }
        private static void AskVehicleInfo(int vehicleType)
        {
            string RegNo = Util.AskForString("-Please enter 'Registration No.' of the vehicle you want to park:"
                                                     + "\n-q: to quit");

            if (RegNo == "q") return;
            else if (garageHandler.RegNoExists(RegNo))
            {
                Console.WriteLine($"This Reg. No already exists! Please try again with another Reg. No.\n");
                return;
            }

            string color = Util.AskForString("Color?:");
            int noOfWheels = Util.AskForInt("No. of wheels?:");

            switch (vehicleType)
            {
                case 1:
                    int noOfEngines = Util.AskForInt("No. of engines?:");

                    if (garageHandler.ParkAirplane(noOfEngines, RegNo, color, noOfWheels))
                        Console.WriteLine($"An airplane with your enterred info parked in the garage!\n");
                    break;
                case 2:
                    int cylinderVol = Util.AskForInt("Cylinder volume?:");

                    if (garageHandler.ParkMotorcycle(cylinderVol, RegNo, color, noOfWheels))
                        Console.WriteLine($"A motorcycle with your enterred info parked in the garage!\n");
                    break;
                case 3:
                    string fuelType = Util.AskForString("Fuel Type?:");

                    if (garageHandler.ParkCar(fuelType, RegNo, color, noOfWheels))
                        Console.WriteLine($"A car with your enterred info parked in the garage!\n");
                    break;
                case 4:
                    int noOfSeats = Util.AskForInt("No. of seats?:");

                    if (garageHandler.ParkBus(noOfSeats, RegNo, color, noOfWheels))
                        Console.WriteLine($"A bus with your enterred info parked in the garage!\n");
                    break;
                case 5:
                    double length = Util.AskForDouble("Length?:");

                    if (garageHandler.ParkBoat(length, RegNo, color, noOfWheels))
                        Console.WriteLine($"A boat with your enterred info parked in the garage!\n");
                    break;
                default:
                    break;
            }
        }

        private static void ParkedVehiclesList()
        {
            //int noOfParkedVehicles = 0;
            //noOfParkedVehicles = Write(garageHandler.GetGarageVehicles(), item => Console.WriteLine($"{item.GetType().Name},  Reg. No. : {item.RegNo}, Color: {item.Color}, No. of wheels: {item.NoOfWheels}"));
            //if (noOfParkedVehicles is 0)
            //    Console.WriteLine("No vehicle is parked in garage!\n");

            string parkedVehicles = writeToConsole(garageHandler.GetGarageVehicles());
            if (parkedVehicles.Equals(""))
                Console.WriteLine("No vehicle is parked in garage!\n");
            else
                Console.WriteLine(parkedVehicles);
        }
        private static void ParkedVehiclesTypeList()
        {
            Vehicle[] vehiclesArray = garageHandler.GetGarageVehicles();
            Dictionary<string, int> vehicleTypesNo = new Dictionary<string, int>() {
                {"Airplane", 0 },
                {"Motorcycle", 0 },
                {"Car", 0 },
                {"Bus", 0 },
                {"Boat", 0 }
            };

            StringBuilder builder = new StringBuilder();
            foreach (Vehicle item in vehiclesArray)
            {
                if (item != null)
                    switch (item.GetType().Name)
                    {
                        case "Airplane":
                            vehicleTypesNo["Airplane"]++;
                            break;

                        case "Motorcycle":
                            vehicleTypesNo["Motorcycle"]++;
                            break;

                        case "Car":
                            vehicleTypesNo["Car"]++;
                            break;

                        case "Bus":
                            vehicleTypesNo["Bus"]++;
                            break;

                        case "Boat":
                            vehicleTypesNo["Boat"]++;
                            break;
                    }
            }

            foreach (var item in vehicleTypesNo)
                if (item.Value > 0)                
                    builder.AppendLine($"{item.Key},   Total: {item.Value}");                

            if (builder.Equals(""))
                Console.WriteLine("No vehicle is parked in garage!");
            else
                Console.WriteLine(builder);
        }
        private static void SearchByProperties()
        {
            while (true)
            {
                string color = Util.AskForString("\n-Please enter 'color' of the vehicle you search for:"
                                                         + "\n-q: to quit");
                if (color == "q") break;

                int noOfWheels = Util.AskForInt("-Please enter No. of wheels' of the vehicle you search for:");
                var vehicles = garageHandler.SearchByProperties(color, noOfWheels);
                if (vehicles.Count.Equals(0))
                    Console.WriteLine("No vehicle with your entered info exists!\n");
                else
                {
                    var builder = new StringBuilder();
                    vehicles.ForEach(v => builder.AppendLine($"{v.GetType().Name},  Reg. No. : {v.RegNo}, Color: {v.Color}, No. of Wheels: {v.NoOfWheels}"));
                    Console.WriteLine(builder);
                }
            }
        }
        private static void SearchByRegNo()
        {
            while (true)
            {
                string regNoInput = Util.AskForString("-Please enter Reg. No. of the vehicle you search for:"
                                                         + "\n-0: to quit");
                if (regNoInput == "0") break;

                try
                {
                    var vehicle = garageHandler.SearchByRegNo(regNoInput);
                    Console.WriteLine($"{vehicle.GetType().Name},  Reg. No. : {vehicle.RegNo}, Color: {vehicle.Color}, No. of wheels: {vehicle.NoOfWheels}\n");
                }

                catch (NullReferenceException)
                {
                    Console.WriteLine("No vehicle with your entered Reg. No exists!\n");
                }
            }
        }
        private static void Unpark()
        {
            while (true)
            {
                string regNoInput = Util.AskForString("-Please enter Reg. No. of the vehicle you want to unpark:"
                                                         + "\n-0: to quit");
                if (regNoInput == "0") break;

                try
                {
                    var vehicle = garageHandler.Unpark(regNoInput);
                    Console.WriteLine($"{vehicle.GetType().Name},  Reg. No. : {vehicle.RegNo}, Color: {vehicle.Color}, No. of wheels: {vehicle.NoOfWheels} unparked successfully\n");
                }

                catch (NullReferenceException)
                {
                    Console.WriteLine("No vehicle with your entered Reg. No exists!\n");
                }
            }
        }
        //private static int Write(Vehicle[] vehicle, Action<Vehicle> action)
        //{
        //    int counter = 0;
        //    foreach (var i in vehicle)
        //    {
        //        if (i != null)
        //        {
        //            action.Invoke(i);
        //            counter++;
        //        }
        //        else
        //            break;
        //    }

        //    return counter;
        //}

        private static Func<Vehicle[], string> writeToConsole = WriteToConsole;

        public static void PrintWhenParked(object sender, GarageEventArgs e)
        {
            Console.WriteLine(writeToConsole.Invoke(e.vehicles));
        }
        private static string WriteToConsole(Vehicle[] vehicles)
        {
            var builder = new StringBuilder();

            foreach (var v in vehicles)
            {
                if (v != null)
                    builder.AppendLine($"{v.GetType().Name},  Reg. No. : {v.RegNo}, Color: {v.Color}, No. of wheels: {v.NoOfWheels}");
                else
                    break;
            }

            return builder.ToString();
        }
        public static void Save()
        {
            //try
            //{
                formatter = new BinaryFormatter();
                //File.SetAttributes(DATA_FILENAME, FileAttributes.Normal);
                var path = Path.Combine(Path.GetTempPath(), "Garage1.dat" );

                FileStream writerFileStream =
                        new FileStream(path, FileMode.Create);
                formatter.Serialize(writerFileStream, garageHandler.Garage);
                writerFileStream.Close();
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("Unable to save Garage information");
            //}

            //XmlSerializer xs = new XmlSerializer(typeof(Garage<Vehicle>));
            //TextWriter txtWriter = new StreamWriter(DATA_FILENAME);
            //xs.Serialize(txtWriter, garageHandler.GetGarageVehicles());
            //txtWriter.Close();
        }
    }

    //enum VehicleType
    //{
    //    Airplane = 1,
    //    Motorcycle = 2,
    //    Car = 3,
    //    Bus = 4,
    //    Boat = 5
    //}
}
