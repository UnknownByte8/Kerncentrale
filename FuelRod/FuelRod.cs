using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerncentrale.FuelRod
{
    public abstract class FuelRod
    {
        private string name;
        private int huidigeTemperatuur;
        private int temperatuur;
        private int optimaleTemperatuur;
        private double graadPerLiter;
        private int overhittingsTemperatuur;
        private int onderLimietTemperatuur;

        public string Name => name;
        public abstract void SetName(string value);

        public int HuidigeTemperatuur => huidigeTemperatuur;

        public abstract void SetHuidigeTemperatuur(int value);

        public int Temperatuur => temperatuur;
        public abstract void SetTemperatuur(int value);

        public int OptimaleTemperatuur => optimaleTemperatuur;
        public abstract void SetOptimaleTemperatuur(int value);

        public double GraadPerLiter => graadPerLiter;
        public abstract void SetGraadPerLiter(double value);

        public int OverhittingsTemperatuur => overhittingsTemperatuur;
        public abstract void SetOverhittingsTemperatuur(int value);

        public int OnderLimietTemperatuur => onderLimietTemperatuur;
        public abstract void SetOnderLimietTemperatuur(int value);
    }   
}
