using System;
using System.Collections.Generic;

namespace Airport_Simulation
{
    /// <summary>
    /// Drew H. Meseck
    /// Professor Owrang
    /// Algorithms and Data Structures
    /// 
    /// This is the main control of the simulation, creating the airport instance and then simulating it.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            Queue<Airplane> takeoff = new Queue<Airplane>();
            Queue<Airplane> landing = new Queue<Airplane>();

            int mtc = 120;

            double top = 1;//priority for takeoffs relative to arrivals
            double ap = 1.5;//priority for arrivals relative to takeoffs

            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            //These numbers were tweaked through optimization to ensure that there is enough priority given
            //to landing planes so that there would be minimal amounts of fuel wasted. This also theoretically
            //minimizes (based on my implementation of runway assignment) the total average wait time given
            //that priority restriction
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

            Airport airport = new Airport(top, ap, takeoff, landing, mtc/5);

            airport.Simulate();
            
        }
    }
}
