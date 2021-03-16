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

        public int Water { get => water; set => water = value; }

        public int DistributeWater()
        {
            return water;
        }
    }
}
