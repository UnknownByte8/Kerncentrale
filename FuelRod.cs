using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerncentrale
{
    interface FuelRod
    {
        public bool Execute();
        public string GetName();
        public int getHuidigeTemperature();
        public void setTemperature(int);
        public int getOptimaleTemperatuur();
        public double getGraadPerLiter();
        public int getOverhittingsTemperatuur();
        public int getOnderLimietTemperatuur();
    }   
}
