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
        private int overhittingstemperatuur;
        private int optimaleTemperatuur;
        private int onderLimietTemperatuur;
        private double graadPerLiter;

        public Plutonium()
        {
            SetName("Plutonium");
            SetHuidigeTemperatuur(0);
            SetOverhittingsTemperatuur(6000);
            SetOptimaleTemperatuur(2500);
            SetOnderLimietTemperatuur(1500);
            SetGraadPerLiter(1.8);
        }

        public override void SetGraadPerLiter(double value) => this.graadPerLiter = value;

        public override void SetHuidigeTemperatuur(int value) => this.huidigeTemperatuur = value;

        public override void SetName(string value) => this.name = value;

        public override void SetOnderLimietTemperatuur(int value) => this.onderLimietTemperatuur = value;

        public override void SetOptimaleTemperatuur(int value) => this.optimaleTemperatuur = value;

        public override void SetOverhittingsTemperatuur(int value) => this.overhittingstemperatuur = value;

        public override void SetTemperatuur(int value) => this.onderLimietTemperatuur = value;
    }
}
