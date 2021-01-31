using System;
using System.Net.Sockets;

namespace TcpTestServer.Competition
{
    class Racer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Car Car { get; set; }


        public Racer(int id, string name, double fuelLoad, Tires.Tires tires)
        {
            ID = id;
            Name = name;
            Car = CreateDefaultCarWithTires(fuelLoad, tires);
          
        }

        private Car CreateDefaultCarWithTires(double fuel, Tires.Tires tires)
        {
            return new Car(fuel, tires);
        }

        public override string ToString()
        {
            return $"{ID} - {Name} ({Car.Tires})";
        }
    }
}
