using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerncentrale.FuelRod
{
    class Plutonium : FuelRod
    {
        private string name;
        private double huidigeTemperatuur;
        private double temperatuur;
        private double optimaleTemperatuur;
        private double graadPerLiter;
        private double overhittingsTemperatuur;
        private double onderLimietTemperatuur;
        private double tempIncrease;


        public Plutonium()
        {
            SetName("Plutonium");
            SetHuidigeTemperatuur(0);
            SetOverhittingsTemperatuur(6000);
            SetOptimaleTemperatuur(2500);
            SetOnderLimietTemperatuur(1500);
            SetGraadPerLiter(1.8);
            SetTempIncrease(3);
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

        public override void AfkoelenMetLitersWater(double waterInLiter){
            SetHuidigeTemperatuur(huidigeTemperatuur - (waterInLiter * graadPerLiter));
        }

        public override void SetGraadPerLiter(double value) => this.graadPerLiter = value;

        public override void SetHuidigeTemperatuur(double value) => this.huidigeTemperatuur = value;

        public override void SetName(string value) => this.name = value;

        public override void SetOnderLimietTemperatuur(double value) => this.onderLimietTemperatuur = value;

        public override void SetOptimaleTemperatuur(double value) => this.optimaleTemperatuur = value;

        public override void SetOverhittingsTemperatuur(double value) => this.overhittingsTemperatuur = value;

        public override void SetTemperatuur(double value) => this.onderLimietTemperatuur = value;

        public override double SetTempIncrease(double value) => this.tempIncrease = value;
    }
}
