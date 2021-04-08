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
        private int huidigeTemperatuur;
        private int temperatuur;
        private int optimaleTemperatuur;
        private double graadPerLiter;
        private int overhittingsTemperatuur;
        private int onderLimietTemperatuur;
        private int tempIncrease;



        public Plutonium()
        {
            SetName("Plutonium");
            SetHuidigeTemperatuur(0);
            SetOverhittingsTemperatuur(6000);
            SetOptimaleTemperatuur(2500);
            SetOnderLimietTemperatuur(1500);
            SetGraadPerLiter(1.8);
        }

        public override void Excecute()
        {
            
            

        }

        public override void SetGraadPerLiter(double value) => this.graadPerLiter = value;

        public override void SetHuidigeTemperatuur(int value) => this.huidigeTemperatuur = value;

        public override void SetName(string value) => this.name = value;

        public override void SetOnderLimietTemperatuur(int value) => this.onderLimietTemperatuur = value;

        public override void SetOptimaleTemperatuur(int value) => this.optimaleTemperatuur = value;

        public override void SetOverhittingsTemperatuur(int value) => this.overhittingsTemperatuur = value;

        public override void SetTemperatuur(int value) => this.onderLimietTemperatuur = value;

        public override int SetTempIncrease(int value) => this.tempIncrease = value;
    }
}
