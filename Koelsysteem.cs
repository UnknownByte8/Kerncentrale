using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerncentrale
{
    class Koelsysteem
    {
        private int water;
        private int waterTemp;

        /// <summary>
        /// Constructor of Koelsysteem
        /// </summary>
        /// <param name="waterTemp">Initial Water temperature</param>
        public Koelsysteem(int waterTemp)
        {
            this.SetWaterTemp(waterTemp);
        }

        public int GetWater()
        {
            return water;
        }

        public void SetWater(int value)
        {
            water = value;
        }

        public int GetWaterTemp()
        {
            return waterTemp;
        }

        private void SetWaterTemp(int value)
        {
            waterTemp = value;
        }

        public int DistributeWater()
        {



            return water;
        }


    }
}
