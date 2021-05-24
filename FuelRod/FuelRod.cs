namespace Kerncentrale.FuelRod
{
    public abstract class FuelRod
    {
        private double literWater;
        private double stoom;
        protected string name;
        /*
         * Meldown will throw an MeltdownExeption
         */
        public void MeltDown() => throw new MeltdownExeption();

        public abstract void Excecute();
        public abstract void AfkoelenMetLitersWater(double waterInLiter);
        public abstract void GenerateSteam(double temperatuur);

        public abstract void SetName(string value);

        public string getName() { return this.name; }

        public abstract double GetHuidigeTemperatuur();

        public abstract double GetOverHittingsTempreatuur();

        public abstract void SetHuidigeTemperatuur(double value);

        public abstract void SetTemperatuur(double value);

        public abstract void SetOptimaleTemperatuur(double value);

        public abstract void SetGraadPerLiter(double value);

        public abstract void SetOverhittingsTemperatuur(double value);

        public double LiterWater { get => literWater; set => literWater = value; }
        public double Stoom { get => stoom; set => stoom = value; }

        public abstract double SetTempIncrease(double value);

        public abstract void SetOnderLimietTemperatuur(double value);
    }
}
