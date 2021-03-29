using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerncentrale.FuelRod
{
    //voorbeeld soort fuelrod
    class Uranium : FuelRod
    {
        private string name;
        private int huidigeTemperatuur;
        private int temperatuur;
        private int optimaleTemperatuur;
        private double graadPerLiter;
        private int overhittingsTemperatuur;
        private int onderLimietTemperatuur;
        private int tempIncrease;

        public Uranium()
        {
            SetName("Uranium");
            SetHuidigeTemperatuur(0);
            SetOverhittingsTemperatuur(5000);
            SetOptimaleTemperatuur(4000);
            SetOnderLimietTemperatuur(3000);
            SetGraadPerLiter(0.5);
            SetTempIncrease(5);
        }

        public override void Excecute()
        {
            if(huidigeTemperatuur < onderLimietTemperatuur){
                 SetHuidigeTemperatuur(huidigeTemperatuur + (tempIncrease * 10));
            } else{
                 SetHuidigeTemperatuur(huidigeTemperatuur + tempIncrease);
            }    
            
            if(huidigeTemperatuur > overhittingsTemperatuur){
             MeltDown();
            }
        }

         public override void AfkoelenMetLitersWater(int waterInLiter){
            SetHuidigeTemperatuur(huidigeTemperatuur - (waterInLiter * graadPerLiter));


        }

        public override void SetGraadPerLiter(double value) => this.graadPerLiter = value;

        public override void SetHuidigeTemperatuur(int value) => this.huidigeTemperatuur = value;

        public override void SetName(string value) => this.name = value;

        public override void SetOnderLimietTemperatuur(int value) => this.onderLimietTemperatuur = value;

        public override void SetOptimaleTemperatuur(int value) => this.optimaleTemperatuur = value;

        public override void SetOverhittingsTemperatuur(int value) => this.overhittingsTemperatuur = value;

        public override void SetTemperatuur(int value) => this.onderLimietTemperatuur = value;
    }
}
