using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Simulation
{
    class Airplane
    {
        int id { get; set; }
        int wait_time { get; set; }
        int status { get; set;}


        public Airplane(int identification, int s)
        {
            this.id = identification;
            this.wait_time = 0;
            this.status = s;

        }

        public void Update()
        {
            this.wait_time++;
        }

        public string getIDString()
        {
            string res = "#" + Convert.ToString(this.id);
            return res;
        }
    }
}
