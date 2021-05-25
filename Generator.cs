namespace Kerncentrale
{
    class Generator
    {
        private double kWh;
        private double totalKWh = 0;
        public double stoom;
        public double TotalKWh { get => totalKWh; set => totalKWh = value; }

        public double GetKWh()
        {
            return kWh;
        }


        /// <summary>
        /// Sets current KWh
        /// </summary>
        /// <param name="value">Value to set KWh to</param>
        private void SetKWh(double value)
        {
            kWh = value;
            TotalKWh += value;
        }

        public void SetStoom(double value)
        {
            this.stoom = value;
        }

        public double GetStoom()
        {
            return this.stoom;
        }

        public Generator()
        {

        }

        /// <summary>
        /// calculate how much energy is created
        /// </summary>
        /// <param name="tmpStoom"></param>
        /// <returns></returns>
        public double GenerateEnergy(double tmpStoom)
        {
            SetStoom(tmpStoom);
            double tmpKwh = (tmpStoom * 9.0);
            SetKWh(tmpKwh);

            return tmpKwh;
        }
    }
}
