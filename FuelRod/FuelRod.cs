namespace Kerncentrale.FuelRod
{
    public abstract class FuelRod
    {
        private string name;
        private double huidigeTemperatuur;
        private double temperatuur;
        private double optimaleTemperatuur;
        private double graadPerLiter;
        private double overhittingsTemperatuur;
        private double onderLimietTemperatuur;
        private double tempIncrease;
        private double literWater;
        private double stoom;
        public void MeltDown()
        {
            throw new MeltdownExeption();
            // Alles kapot            
        }

        public abstract void Excecute();
        public abstract void AfkoelenMetLitersWater(double waterInLiter);
        public abstract void GenerateSteam(double temperatuur);

        public string Name => name;
        public abstract void SetName(string value);

        public abstract double GetHuidigeTemperatuur();


        public abstract void SetHuidigeTemperatuur(double value);

        public double Temperatuur => temperatuur;
        public abstract void SetTemperatuur(double value);

        public double OptimaleTemperatuur => optimaleTemperatuur;
        public abstract void SetOptimaleTemperatuur(double value);

        public double GraadPerLiter => graadPerLiter;
        public abstract void SetGraadPerLiter(double value);

        public double OverhittingsTemperatuur => overhittingsTemperatuur;
        public abstract void SetOverhittingsTemperatuur(double value);

        public double OnderLimietTemperatuur => onderLimietTemperatuur;

        public double TempIncrease => tempIncrease;

        public double LiterWater { get => literWater; set => literWater = value; }
        public double Stoom { get => stoom; set => stoom = value; }

        public abstract double SetTempIncrease(double value);

        public abstract void SetOnderLimietTemperatuur(double value);
    }
}
