using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Kerncentrale
{
    class Reactor
    {
        private List<FuelRod.FuelRod> fuelRods = new List<FuelRod.FuelRod>();
        private ThreadingType selectedThreadingType;
        private Generator generator;
        public Reactor()
        {
            this.generator = new Generator();
        }

        /*
         * set the selected TreadingType 
         */
        public void setSelectedThreadingType(ThreadingType threadingType)
        {
            this.selectedThreadingType = threadingType;
        }

        public void addFuelRod(FuelRod.FuelRod fuelRod)
        {
            this.fuelRods.Add(fuelRod);
        }

        /*
         * every fuelrods will be cooled of with an assigned amount of water
         */
        public void koelFuelrods(int water)
        {
            foreach (FuelRod.FuelRod fuelrod in fuelRods)
            {
                fuelrod.LiterWater = water;
            }
        }
        public double getWaterFuelRods()
        {
            return this.fuelRods[0].LiterWater;
        }

        /*
         * create and return the amount of energy gotton out of steam
         */
        public double getEnergy()
        {
            double tmpStoom = 0;
            double energy = 0;
            foreach (FuelRod.FuelRod fuelrod in fuelRods)
            {
                tmpStoom += fuelrod.Stoom;
            }
            if (tmpStoom != 0 && tmpStoom > 0)
            {
                generator.GenerateEnergy(tmpStoom);
                energy = generator.GetKWh();
            }
            return energy;
        }

        public void threadProcess(Object stateInfo)
        {
            executeThread();
            ThreadPool.QueueUserWorkItem(threadProcess);
        }

        /*
         * execute thread
         * when MeltdownExeption is thrown the program will shut down becouse the whole kerncentrale exploded.
         */
        public void executeThread()
        {
            try
            {
                foreach (FuelRod.FuelRod fuelRod in fuelRods)
                {
                    fuelRod.Excecute();
                }
            }
            catch (MeltdownExeption e)
            {
                Debug.WriteLine("Kerncentrale is geexplodeeerd door een meltdown in een reactor.");
                Environment.Exit(Environment.ExitCode);
                return;
            }
            Thread.Sleep(1000);

            if (this.selectedThreadingType == ThreadingType.MultiThreading)
            {
                Thread thread = new Thread(executeThread);
                thread.Name = this.ToString();
                try
                {
                    thread.Start();
                }
                catch (MeltdownExeption e)
                {
                    thread.Abort();
                }
            }
        }
    }
}
