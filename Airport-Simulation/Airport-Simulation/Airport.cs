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
    /// This portion of the program definies what constitutes an instance of an airport, manages its direct functions
    /// and actually simulates its operations. It is also responsible for the generation of airplanes, and the 
    /// maintainance of the clock and all statistics.
    /// </summary>
    class Airport
    {
        //DEFINE ALL ATTRIBUTES
        Queue<Airplane> takeoff;
        Queue<Airplane> landing;
        List<Airplane> processed; 

        Boolean runway0;
        Boolean runway1;

        double top;
        double ap;

        int min_t_c;

        double ato_wait;
        double aa_wait;
        int num_to;
        int num_ld;

        int clock;
        int id_counter_arr;
        int id_counter_dep;
        //END DEFINE ATTRIBUTES
        public Airport(double takeoff_propensity, double arrival_propensity,
            Queue<Airplane> to, Queue<Airplane> ld, int minutes_to_complete)//Creates an Airport instance
        {
            this.takeoff = to;
            this.landing = ld;
            this.processed = new List<Airplane>();
            this.top = takeoff_propensity;
            this.ap = arrival_propensity;
            this.min_t_c = minutes_to_complete;
            this.clock = 0;
            this.id_counter_arr = 1;
            this.id_counter_dep = 0;
            this.num_ld = 0;
            this.num_to = 0;
        }

        public void IncrementTime()//Increments the clock
        {
            this.clock++;
        }

        public string GetTime()//Returns the time from the clock as a string (more readable to people) starting at noon.
        {
            int min_elapsed = this.clock * 5;
            int hour_counter = min_elapsed / 60;
            int start_hour = 12;
            min_elapsed = min_elapsed - (hour_counter * 60);

            start_hour += hour_counter;

            string time = Convert.ToString(start_hour) + ":" + Convert.ToString(min_elapsed) + "PM";

            return time;

        }

        public List<Airplane> GenerateTO(double priority)//Generates Takeoffs
        {
            Random rand = new Random();
            List<Airplane> results = new List<Airplane>();

            int num = rand.Next(0, 3);

            for(int i = 0; i < num; i++)
            {
                results.Add(new Airplane(id_counter_dep, 1));
                this.id_counter_dep += 2;
            }

            return results;
        }

        public List<Airplane> GenerateArr(double priority)//Generates Arrivals
        {
            Random rand = new Random();
            List<Airplane> results = new List<Airplane>();

            int num = rand.Next(0, 3);

            for (int i = 0; i < num; i++)
            {
                results.Add(new Airplane(id_counter_arr, 2));
                this.id_counter_arr += 2;
            }

            return results;
        }

        public void UpdatePlanes()//Increments the wait time for planes in queues and prints updates as to the queue sizes
        {
            foreach(Airplane plane in this.takeoff)
                plane.Update();
            Console.WriteLine($"The number of planes waiting to takeoff is: {this.takeoff.Count}");

            foreach(Airplane plane in this.landing)
                plane.Update();
            Console.WriteLine($"The number of planes waiting to land is: {this.landing.Count}");

        }

        public void Aggregate(Queue<Airplane> takeoff, Queue<Airplane> landing, List<Airplane> processed) //Aggregates and averages relevant statistics
        {
            int to_wait_sum = 0;
            int a_wait_sum = 0;
            int to_count = 0;
            int a_count = 0;
            foreach(Airplane plane in processed)
            {
                if (plane.status == 1)
                {
                    to_wait_sum += plane.wait_time;
                    to_count++;
                }
                if(plane.status == 2)
                {
                    a_wait_sum += plane.wait_time;
                    a_count++;
                }
            }
            
            foreach(Airplane plane in takeoff)
            {
                if (plane.status == 1)
                {
                    to_wait_sum += plane.wait_time;
                    to_count++;
                }
                if (plane.status == 2)
                {
                    a_wait_sum += plane.wait_time;
                    a_count++;
                }
            }
            foreach(Airplane plane in landing)
            {
                if (plane.status == 1)
                {
                    to_wait_sum += plane.wait_time;
                    to_count++;
                }
                if (plane.status == 2)
                {
                    a_wait_sum += plane.wait_time;
                    a_count++;
                }
            }

            this.aa_wait = (double)a_wait_sum / (double)a_count;
            this.ato_wait = to_wait_sum / to_count;

        }

        public void PrintStatistics(Queue<Airplane> takeoff, Queue<Airplane> landing,
            double average_takeoff_wait, double average_landing_wait, int total_takeoffs_processed,
            int total_landings_processed)//Print formatting for the aggregated statistics (Prints the final output of the simulation)
        {
            Console.WriteLine();
            Console.WriteLine("-------------------FINAL STATISTICS-------------------");
            Console.WriteLine();
            Console.WriteLine("The Planes waiting for Takeoff are: ");
            foreach(Airplane plane in takeoff)
            {
                Console.Write($"{plane.GetIDString()}, ");
            }
            Console.WriteLine();
            Console.WriteLine($"The Average time Planes waited to takeoff was: {average_takeoff_wait}");
            Console.WriteLine("The Planes waiting for Landing are: ");
            foreach (Airplane plane in landing)
            {
                Console.Write($"{plane.GetIDString()}, ");
            }
            Console.WriteLine();
            Console.WriteLine($"The Average time Planes waited to land was: {average_landing_wait}");
            Console.WriteLine($"The total number of planes processed was: {this.processed.Count}");
            Console.WriteLine($"The total number of planes landed was: {total_landings_processed}");
            Console.WriteLine($"The total number of planes departed was: {total_takeoffs_processed}");

            double atw = (average_landing_wait + average_takeoff_wait);
            atw = atw / 2;

            Console.WriteLine($"The Average Total Wait Time is {atw}");

        }

        public void Simulate() //Actually simulates the generation of planes and allocation of runways based on a clock cycle.
        {
            int ratio_flag = 0;// 0 means more planes are arriving than departing (default priority)
            if (ap < top)
                ratio_flag = 1;// 1 means more planes are departing than arriving (deviant priority)

            while (this.clock < this.min_t_c)//While the clock is less than the given time to complete do:
            {
                //Generate new lists of arrivals and departures
                List<Airplane> arrivals = GenerateArr(this.ap);
                List<Airplane> departures = GenerateTO(this.top);

                //Add the new planes to the queues
                foreach (Airplane plane in arrivals)
                    this.landing.Enqueue(plane);
                foreach (Airplane plane in departures)
                    this.takeoff.Enqueue(plane);

                //Initial Printing for each Clocked print
                Console.WriteLine();
                Console.WriteLine($"The time is: {this.GetTime()}");
                Console.WriteLine($"There are {this.takeoff.Count} planes waiting to depart");
                Console.WriteLine($"There are {this.landing.Count} planes waiting to land");


                //Assign runway0 
                if(this.runway0 == true && (this.landing.Count != 0 || this.takeoff.Count !=0))
                {
                    if(this.landing.Count == 0)//if landing is empty assign a takeoff
                    {
                        Airplane temp = this.takeoff.Dequeue();
                        Console.WriteLine($"Plane {temp.GetIDString()} is cleared for Takeoff");
                        this.processed.Add(temp);
                        this.num_to++;
                        this.runway0 = false;
                    }
                    else if(takeoff.Count == 0 && this.runway0 != false)//if takeoff is empty assign a landing
                    {
                        Airplane temp = this.landing.Dequeue();
                        Console.WriteLine($"Plane {temp.GetIDString()} is cleared for Landing");
                        this.processed.Add(temp);
                        this.num_ld++;
                        this.runway0 = false;
                    }
                    else if(ratio_flag == 0 && this.runway0 != false)//more landing than departing (priority assignment)
                    {
                        Airplane temp = this.landing.Dequeue();
                        Console.WriteLine($"Plane {temp.GetIDString()} is cleared for Landing");
                        this.processed.Add(temp);
                        this.num_ld++;
                        this.runway0 = false;
                    }
                    else if(ratio_flag == 1 && this.runway0 != false)
                    {
                        Airplane temp = this.takeoff.Dequeue();
                        Console.WriteLine($"Plane {temp.GetIDString()} is cleared for Takeoff");
                        this.processed.Add(temp);
                        this.num_to++;
                        this.runway0 = false;
                    }
                }
                //Assign runway1 
                if (this.runway1 == true && (this.landing.Count != 0 || this.takeoff.Count != 0))
                {
                    if (this.landing.Count == 0)//if landing is empty assign a takeoff
                    {
                        Airplane temp = this.takeoff.Dequeue();
                        Console.WriteLine($"Plane {temp.GetIDString()} is cleared for Takeoff");
                        this.processed.Add(temp);
                        this.num_to++;
                        this.runway1 = false;
                    }
                    else if (takeoff.Count == 0 && this.runway0 != false)//if takeoff is empty assign a landing
                    {
                        Airplane temp = this.landing.Dequeue();
                        Console.WriteLine($"Plane {temp.GetIDString()} is cleared for Landing");
                        this.processed.Add(temp);
                        this.num_ld++;
                        this.runway1 = false;
                    }
                    else if(this.runway1 != false && (this.landing.Count != 0 || this.takeoff.Count != 0))//pseudo random assignment (mostly takeoffs though)
                    {
                        Random rand = new Random();
                        double r = rand.NextDouble();
                        if(r < .25 && ratio_flag == 0)
                        {
                            Airplane temp = this.landing.Dequeue();
                            Console.WriteLine($"Plane {temp.GetIDString()} is cleared for Landing");
                            this.processed.Add(temp);
                            this.num_ld++;
                            this.runway0 = false;
                        }
                        else
                        {
                            Airplane temp = this.takeoff.Dequeue();
                            Console.WriteLine($"Plane {temp.GetIDString()} is cleared for Takeoff");
                            this.processed.Add(temp);
                            this.num_to++;
                            this.runway0 = false;
                        }
                    }
                }

                //End of clock updates
                this.UpdatePlanes();
                Console.WriteLine();
                this.IncrementTime();
                this.runway0 = true;
                this.runway1 = true;
            }
            //End of simulation statistical aggregation and printing
            this.Aggregate(this.takeoff, this.landing, this.processed);
            this.PrintStatistics(this.takeoff, this.landing, this.ato_wait, this.aa_wait,
                this.num_to, this.num_ld);
            


        }



    }
}
