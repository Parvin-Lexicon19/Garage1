namespace Garage1
{
    public class Vehicle
    {
        public string RegNo { get; set; }
        public string Color { get; set; }
        public int NoOfWheels { get; set; }

        public Vehicle(string regNo, string color, int noOfWheels)
        {
            RegNo = regNo;
            Color = color;
            NoOfWheels = noOfWheels;
        }
    }
}