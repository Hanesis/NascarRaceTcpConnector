namespace TcpTestServer.Competition
{
    class Car
    {
        public Tires.Tires Tires { get; set; }
       

        public Car(double actualFuel, Tires.Tires tires)
        {
            Tires = tires;
        }
    }
}
