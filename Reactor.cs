using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

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

        public void IncTempIncrease(int increase)
        {
            foreach (FuelRod.FuelRod fuelrod in fuelRods)
            {
                fuelrod.SetTempIncrease(fuelrod.TempIncrease+increase);
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
                if (this.selectedThreadingType == ThreadingType.MultiThreading)
                {
                    foreach (var item in fuelRods)
                    {
                        Task.Factory.StartNew(() => ExecuteThreadMulti(item));
                    }
                }
                else if (this.selectedThreadingType == ThreadingType.SingleThreading)
                {
                    Task t2 = Task.Factory.StartNew(ExecuteThreadSingle);
                }
            }
            catch (MeltdownExeption e)
            {
                Debug.WriteLine("Kerncentrale is geexplodeeerd door een meltdown in een reactor.\n"+e);

                Environment.Exit(Environment.ExitCode);
                return;
            }
        }

        private void ExecuteThreadMulti(object fuelRod)
        {
            if (fuelRod.GetType() == typeof(FuelRod.Plutonium))
            {
                var x = (fuelRod as FuelRod.Plutonium);
                x.Excecute();
            }
            if (fuelRod.GetType() == typeof(FuelRod.Uranium))
            {
                var x = (fuelRod as FuelRod.Uranium);

                x.Excecute();
            }
        }

        private void ExecuteThreadSingle()
        {
            foreach (FuelRod.FuelRod fuelRod in fuelRods)
            {
                fuelRod.Excecute();
            }
        }
    }
}
