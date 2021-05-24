using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Kerncentrale
{
    class Reactor
    {
        private List<FuelRod.FuelRod> fuelRods = new List<FuelRod.FuelRod>();
        public ThreadingType selectedThreadingType;
        private Generator generator;
        public double energy = 0;
        public Reactor()
        {
            this.generator = new Generator();
        }

        /*
         * set the selected TreadingType 
         */
        public void SetSelectedThreadingType(ThreadingType threadingType)
        {
            this.selectedThreadingType = threadingType;
        }

        public void AddFuelRod(FuelRod.FuelRod fuelRod)
        {
            this.fuelRods.Add(fuelRod);
        }

        /*
         * every fuelrods will be cooled of with an assigned amount of water
         */
        public void KoelFuelrods(int water)
        {
            foreach (FuelRod.FuelRod fuelrod in fuelRods)
            {
                fuelrod.LiterWater = water;
            }
        }
        public double GetWaterFuelRods()
        {
            return this.fuelRods[0].LiterWater;
        }

        /*
         * create and return the amount of energy gotton out of steam
         */
        public double GetEnergy()
        {
            double tmpStoom = 0;
            foreach (FuelRod.FuelRod fuelrod in fuelRods)
            {
                tmpStoom += fuelrod.Stoom;
            }
            if (tmpStoom != 0 && tmpStoom > 0)
            {
                generator.GenerateEnergy(tmpStoom);
                this.energy = generator.GetKWh();
            }
            return this.energy;
        }

        public void ThreadProcess(Object stateInfo)
        {
            ExecuteThread();
            ThreadPool.QueueUserWorkItem(ThreadProcess);
        }

        /*
         * execute thread
         * when MeltdownExeption is thrown the program will shut down becouse the whole kerncentrale exploded.
         */
        public void ExecuteThread()
        {
            try
            {
                foreach (FuelRod.FuelRod fuelRod in fuelRods)
                {
                    fuelRod.Excecute();
                }
                if (this.selectedThreadingType == ThreadingType.MultiThreading)
                {
                    Thread thread = new Thread(ExecuteThread);
                    thread.Name = this.ToString();
                    try
                    {
                        thread.Start();
                    }
                    catch (MeltdownExeption e)
                    {
                        Debug.WriteLine("Kerncentrale is geexplodeeerd door een meltdown in een reactor.\n"+ e);

                        thread.Abort();
                        Environment.Exit(Environment.ExitCode);
                        return;
                    }
                }

            }
            catch (MeltdownExeption e)
            {
                Debug.WriteLine("Kerncentrale is geexplodeeerd door een meltdown in een reactor.\n"+e);

                Environment.Exit(Environment.ExitCode);
                return;
            }
            //Thread.Sleep(1000);

            
        }
    }
}
