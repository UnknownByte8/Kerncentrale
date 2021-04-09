namespace Kerncentrale
{
    class Koelsysteem
    {
        private double water;
        private double waterTemp;

        public double Temp { get; set; }

        /// <summary>
        /// Constructor of Koelsysteem
        /// </summary>
        /// <param name="waterTemp">Initial Water temperature</param>
        public Koelsysteem(int waterTemp)
        {
            this.SetWaterTemp(waterTemp);
        }

        public double GetWater()
        {
            return water;
        }

        public void SetWater(double value)
        {
            water = value;
        }

        public double GetWaterTemp()
        {
            return waterTemp;
        }

        private void SetWaterTemp(double restWaarde)
        {
            waterTemp = restWaarde * 0.40;
        }

        public double DistributeWater()
        {
            return water;
        }


    }
}
