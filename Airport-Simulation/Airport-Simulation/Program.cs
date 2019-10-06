using System;
using System.Collections.Generic;

namespace Airport_Simulation
{
    class Program
    {
        static void Main(string[] args)
        {

            Queue<Airplane> takeoff = new Queue<Airplane>();
            Queue<Airplane> landing = new Queue<Airplane>();

            int mtc = 120;

            double top = 1;
            double ap = 1.5;


            Airport airport = new Airport(top, ap, takeoff, landing, mtc/5);

            airport.Simulate();
            
        }
    }
}
