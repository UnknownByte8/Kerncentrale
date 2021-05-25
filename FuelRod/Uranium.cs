using System;
using System.Diagnostics;

namespace Kerncentrale.FuelRod
{
    class Uranium : FuelRod
    {
        private double huidigeTemperatuur;
        private double optimaleTemperatuur;
        private double graadPerLiter;
        private double overhittingsTemperatuur;
        private double onderLimietTemperatuur;
        private double tempIncrease;

        /// <summary>
        /// assign data to uranium
        /// </summary>
        public Uranium()
        {
            SetName("Uranium");
            SetHuidigeTemperatuur(200);
            SetOverhittingsTemperatuur(3500);
            SetOptimaleTemperatuur(3200);
            SetOnderLimietTemperatuur(3000);
            SetGraadPerLiter(1.9);
            SetTempIncrease(5);
            LiterWater = 10;

        }

        /// <summary>
        /// This wil get executed to increase the temperature or cool 
        /// Will throw meltdown when to hot
        /// </summary>
        public override void Excecute()
        {
            if (huidigeTemperatuur < onderLimietTemperatuur)
            {
                SetHuidigeTemperatuur(huidigeTemperatuur + (tempIncrease * 10));
            }
            else if (huidigeTemperatuur < overhittingsTemperatuur)
            {
                SetHuidigeTemperatuur(huidigeTemperatuur + tempIncrease);
            }

            if (huidigeTemperatuur >= overhittingsTemperatuur)
            {
                try
                {
                    MeltDown();
                }
                catch (MeltdownExeption e)
                {
                    Debug.WriteLine("Kerncentrale is geexplodeeerd door een meltdown in een reactor.\n" + e);
                    Environment.Exit(Environment.ExitCode);
                    return;
                }

            }
            AfkoelenMetLitersWater(this.LiterWater);


        }

        /// <summary>
        /// Cools the reactor with some water
        /// </summary>
        /// <param name="waterInLiter">Wil decide how much cooling is done</param>
        public override void AfkoelenMetLitersWater(double waterInLiter)
        {
            SetHuidigeTemperatuur(huidigeTemperatuur - (waterInLiter * graadPerLiter));

            GenerateSteam(GetHuidigeTemperatuur());
        }

        /// <summary>
        ///  Steam will be created so energy will come to existence 
        /// </summary>
        /// <param name="temperatuur">Decides how much steam/energy is created</param>
        public override void GenerateSteam(double temperatuur)
        {
            double tmp = temperatuur * 0.1;

            Stoom = tmp;

        }

        public override void SetGraadPerLiter(double value) => this.graadPerLiter = value;

        public override void SetHuidigeTemperatuur(double value) => this.huidigeTemperatuur = value;
        public override double GetHuidigeTemperatuur()
        {
            return this.huidigeTemperatuur;
        }
        public override double GetOverHittingsTempreatuur()
        {
            return this.overhittingsTemperatuur;
        }

        public override void SetName(string value) => this.name = value;

        public override void SetOnderLimietTemperatuur(double value) => this.onderLimietTemperatuur = value;

        public override void SetOptimaleTemperatuur(double value) => this.optimaleTemperatuur = value;

        public override void SetOverhittingsTemperatuur(double value) => this.overhittingsTemperatuur = value;

        public override void SetTemperatuur(double value) => this.onderLimietTemperatuur = value;

        public override double SetTempIncrease(double value) => this.tempIncrease = value;
    }
}
