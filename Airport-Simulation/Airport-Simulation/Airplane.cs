using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Simulation
{
    class Airplane
    {
        public int id { get; set; }
        public int wait_time { get; set; }
        public int status { get; set;}


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

        public string GetIDString()
        {
            string res = "#" + Convert.ToString(this.id);
            return res;
        }
    }
}
