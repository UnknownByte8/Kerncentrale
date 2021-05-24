namespace Kerncentrale.FuelRod
{
    //voorbeeld soort fuelrod
    class Uranium : FuelRod
    {
        private string name;
        private double huidigeTemperatuur;
        private double optimaleTemperatuur;
        private double graadPerLiter;
        private double overhittingsTemperatuur;
        private double onderLimietTemperatuur;
        private double tempIncrease;

        public Uranium()
        {
            /*
             * assign data to uranium
             */
            SetName("Uranium");
            SetHuidigeTemperatuur(20);
            SetOverhittingsTemperatuur(60000);
            SetOptimaleTemperatuur(4000);
            SetOnderLimietTemperatuur(3000);
            SetGraadPerLiter(0.5);
            SetTempIncrease(5);
            LiterWater = 20;

        }

        /*
        * This wil get executed to increase the temperature or cool 
        */
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
                MeltDown();
            }
            AfkoelenMetLitersWater(this.LiterWater);


        }

        public override void AfkoelenMetLitersWater(double waterInLiter)
        {
            SetHuidigeTemperatuur(huidigeTemperatuur - (waterInLiter * graadPerLiter));

            GenerateSteam(GetHuidigeTemperatuur());
        }

        /*
         * Steam will be created so energy will come to existence 
         */
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

        public override void SetName(string value) => this.name = value;

        public override void SetOnderLimietTemperatuur(double value) => this.onderLimietTemperatuur = value;

        public override void SetOptimaleTemperatuur(double value) => this.optimaleTemperatuur = value;

        public override void SetOverhittingsTemperatuur(double value) => this.overhittingsTemperatuur = value;

        public override void SetTemperatuur(double value) => this.onderLimietTemperatuur = value;

        public override double SetTempIncrease(double value) => this.tempIncrease = value;
    }
}
