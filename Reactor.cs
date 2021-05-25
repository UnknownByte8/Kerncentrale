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
        public double totalEnergy;
        public Reactor()
        {
            this.generator = new Generator();
            SetTotalEnergy(0);
        }

        /// <summary>
        /// set the selected TreadingType 
        /// </summary>
        /// <param name="threadingType"></param>
        public void SetSelectedThreadingType(ThreadingType threadingType)
        {
            this.selectedThreadingType = threadingType;
        }

        /// <summary>
        /// Add a fuelrod to the list
        /// </summary>
        /// <param name="fuelRod"></param>
        public void AddFuelRod(FuelRod.FuelRod fuelRod)
        {
            this.fuelRods.Add(fuelRod);
        }

        /// <summary>
        /// Set total energy (to 0)
        /// </summary>
        /// <param name="value"></param>
        public void SetTotalEnergy(double value) => this.totalEnergy = value;

        /// <summary>
        /// Return total energy of this reactor
        /// </summary>
        /// <returns></returns>
        public double GetTotalEnergy()
        {
            return this.totalEnergy;
        }

        /// <summary>
        /// every fuelrods will be cooled of with an assigned amount of water
        /// </summary>
        /// <param name="water"></param>
        public void KoelFuelrods(int water)
        {
            foreach (FuelRod.FuelRod fuelrod in fuelRods)
            {
                fuelrod.LiterWater = water;
            }
        }

        /// <summary>
        /// Return amount of water on feulrods
        /// </summary>
        /// <returns></returns>
        public double GetWaterFuelRods()
        {
            return this.fuelRods[0].LiterWater;
        }

        /// <summary>
        /// Return amount of temperature on fuelrods
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public double GetTemperatureFuelRods(int i)
        {
            return fuelRods[i].GetHuidigeTemperatuur();
        }

        /// <summary>
        /// Return pverheating temperature of fuelrods
        /// </summary>
        /// <returns></returns>
        public double GetOverheatTemperatureFuelRods()
        {
            return fuelRods[0].GetOverHittingsTempreatuur();
        }

        /// <summary>
        /// create and return the amount of energy gotton out of steam
        /// </summary>
        /// <returns></returns>
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
            totalEnergy += this.energy;
            return this.energy;
        }

        /// <summary>
        /// Start thread process
        /// </summary>
        /// <param name="stateInfo"></param>
        public void ThreadProcess(Object stateInfo)
        {
            ExecuteThread();
            ThreadPool.QueueUserWorkItem(ThreadProcess);
        }

        /// <summary>
        /// Executes thread
        /// when MeltdownExeption is thrown the program will shut down becouse the whole kerncentrale exploded.
        /// </summary>
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
                Debug.WriteLine("Kerncentrale is geexplodeeerd door een meltdown in een reactor.\n" + e);

                Environment.Exit(Environment.ExitCode);
                return;
            }
        }

        /// <summary>
        /// Executes Threads in Multithreading
        /// </summary>
        /// <param name="fuelRod"></param>
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

        /// <summary>
        /// increases Temperature of fuelrods
        /// </summary>
        /// <param name="increase"></param>
        public void IncTempIncrease(int increase)
        {
            foreach (FuelRod.FuelRod fuelrod in fuelRods)
            {
                fuelrod.SetTempIncrease(fuelrod.TempIncrease + increase);
            }
        }

        /// <summary>
        /// Execute 1 single thread
        /// </summary>
        private void ExecuteThreadSingle()
        {
            foreach (FuelRod.FuelRod fuelRod in fuelRods)
            {
                fuelRod.Excecute();
            }
        }
    }
}
