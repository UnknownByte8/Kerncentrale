using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public void setSelectedThreadingType(ThreadingType threadingType)
        {
            this.selectedThreadingType = threadingType;
        }
        public void addFuelRod(FuelRod.FuelRod fuelRod)
        {
            this.fuelRods.Add(fuelRod);
        }

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
        public double getEnergy()
        {
            double tmpStoom = 0;
            double energy = 0;
            foreach (FuelRod.FuelRod fuelrod in fuelRods)
            {
                tmpStoom += fuelrod.Stoom; 
            }
            if(tmpStoom != 0 && tmpStoom > 0)
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
        public void executeThread()
        {
            foreach (FuelRod.FuelRod fuelRod in fuelRods)
            {
                fuelRod.Excecute();
                Debug.WriteLine("FuelRod {" + fuelRod.ToString() + "} is being executed");
            }
            Thread.Sleep(1000);

            if (this.selectedThreadingType == ThreadingType.MultiThreading)
            {
                Thread thread = new Thread(executeThread);
                thread.Name = this.ToString();
                thread.Start();
            }

        }
    }
}
