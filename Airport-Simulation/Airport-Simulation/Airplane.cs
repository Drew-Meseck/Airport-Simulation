using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Simulation
{
    /// <summary>
    /// Drew H. Meseck
    /// Professor Owrang
    /// Algorithms and Data Structures
    /// 
    /// A simple representation of an airplane holding its ID, wait time, and landing/takeoff status.
    /// This is used as an abstraction for holding within the queues as well as a container for 
    /// statistics that the airport will later aggregate.
    /// </summary>
    class Airplane
    {
        public int id { get; set; }
        public int wait_time { get; set; }
        public int status { get; set;}


        public Airplane(int identification, int s)//creates an airplane instance
        {
            this.id = identification;
            this.wait_time = 0;
            this.status = s;

        }

        public void Update()//updates the wait time of an airplane
        {
            this.wait_time++;
        }

        public string GetIDString()//converts the ID to a string and returns it.
        {
            string res = "#" + Convert.ToString(this.id);
            return res;
        }
    }
}
