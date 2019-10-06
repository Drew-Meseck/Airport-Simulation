using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Simulation
{
    class Airport
    {
        Queue<Airplane> takeoff;
        Queue<Airplane> landing;
        List<Airplane> processed; 

        Boolean runway0;
        Boolean runway1;

        double top;
        double ap;

        int min_t_c;

        int ato_wait;
        int aa_wait;

        int clock;
        int id_counter_arr;
        int id_counter_dep;

        public Airport(double takeoff_propensity, double arrival_propensity,
            Queue<Airplane> to, Queue<Airplane> ld, int minutes_to_complete)
        {
            this.takeoff = to;
            this.landing = ld;
            this.top = takeoff_propensity;
            this.ap = arrival_propensity;
            this.min_t_c = minutes_to_complete;
            this.clock = 0;
            this.id_counter_arr = 1;
            this.id_counter_dep = 0;
        }

        public void IncrementTime()
        {
            this.clock++;
        }

        public string GetTime()
        {
            int min_elapsed = this.clock * 5;
            int hour_counter = min_elapsed / 60;
            int start_hour = 12;
            min_elapsed = min_elapsed - (hour_counter * 60);

            start_hour += hour_counter;

            string time = Convert.ToString(start_hour) + ":" + Convert.ToString(min_elapsed) + "PM";

            return time;

        }

        public List<Airplane> GenerateTO(double propensity)//Generates Takeoffs based on propensity to depart
        {
            Random rand = new Random();
            List<Airplane> results = new List<Airplane>();

            double flag = rand.NextDouble();
            if (flag <= .1)
                return results;

            double planes = rand.NextDouble();

            int prop_int = (int)(propensity);

            double int_propensity = (double)prop_int;

            if(planes <= propensity - int_propensity)
            {
                results.Add(new Airplane(id_counter_dep, 1)); //TO status = 1
                this.id_counter_dep += 2;
            }

            for(int i = 0; i < prop_int; i++)
            {
                results.Add(new Airplane(id_counter_dep, 1));
                this.id_counter_dep += 2;
            }

            return results;
        }

        public List<Airplane> GenerateArr(double propensity)//Generates Arrivals based propensity to arrive
        {
            Random rand = new Random();
            List<Airplane> results = new List<Airplane>();

            double flag = rand.NextDouble();
            if (flag <= .1)
                return results;

            double planes = rand.NextDouble();

            int prop_int = (int)(propensity);

            double int_propensity = (double)prop_int;

            if (planes <= propensity - int_propensity)
            {
                results.Add(new Airplane(id_counter_arr, 2)); //A status = 2
                this.id_counter_arr += 2;
            }

            for (int i = 0; i < prop_int; i++)
            {
                results.Add(new Airplane(id_counter_arr, 2));
                this.id_counter_arr += 2;
            }

            return results;
        }

        public void UpdatePlanes()
        {
            foreach(Airplane plane in this.takeoff)
                plane.Update();
            Console.WriteLine($"The number of planes waiting to takeoff is: {this.takeoff.Count}");

            foreach(Airplane plane in this.landing)
                plane.Update();
            Console.WriteLine($"The number of planes waiting to land is: {this.landing.Count}");

        }

        public void Simulate()
        {

            while (this.clock < this.min_t_c)
            {
                List<Airplane> arrivals = GenerateArr(this.ap);
                List<Airplane> departures = GenerateTO(this.top);
                foreach (Airplane plane in arrivals)
                    landing.Enqueue(plane);
                foreach (Airplane plane in departures)
                    takeoff.Enqueue(plane);

                Console.WriteLine($"The time is: {this.GetTime()}");
                Console.WriteLine($"There are {takeoff.Count} planes waiting to depart");
                Console.WriteLine($"There are {landing.Count} planes waiting to land");

                

                

                this.IncrementTime();
            }
            


        }



    }
}
