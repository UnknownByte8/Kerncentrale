using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerncentrale
{
    class Reactor : Controllable
    {
        private List<FuelRod> fuelRods = new List<FuelRod>();

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

    }
}
