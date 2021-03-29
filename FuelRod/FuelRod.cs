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
        private int tempIncrease;

        public void MeltDown(){
              // Alles kapot
        }
        
        public abstract void Excecute();
        public abstract void AfkoelenMetLitersWater(int waterInLiter);

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

        public int TempIncrease => tempIncrease;
        public abstract int SetTempIncrease(int value);

        public abstract void SetOnderLimietTemperatuur(int value);
    }   
}
