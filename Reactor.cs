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
        private List<FuelRod> fuelRods = new List<FuelRod>();

        public void addFuelRod(FuelRod fuelRod)
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
            Debug.WriteLine("Hello this process is being executed");
            Debug.WriteLine("StateInfo {" + stateInfo + "}");
            Thread.Sleep(1000);
            ThreadPool.QueueUserWorkItem(threadProcess);
        }

    }
}
