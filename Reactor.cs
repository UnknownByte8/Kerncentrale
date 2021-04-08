using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Kerncentrale
{
    class Reactor : Controllable
    {
        private List<FuelRod.FuelRod> fuelRods = new List<FuelRod.FuelRod>();
        private ThreadingType selectedThreadingType;

        public void setSelectedThreadingType(ThreadingType threadingType)
        {
            this.selectedThreadingType = threadingType;
        }
        public void addFuelRod(FuelRod.FuelRod fuelRod)
        {
            this.fuelRods.Add(fuelRod);
        }

        public void ChangeWater()
        {
            throw new NotImplementedException();
        }

        public void ExecutekillSwitch()
        {
            throw new NotImplementedException();
        }

        public void GetTemperatuur()
        {
            throw new NotImplementedException();
        }

        public void TestSwtitch()
        {
            throw new NotImplementedException();
        }

        public int generateSteam()
        {
            int stoom = 1;
            return stoom;
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
