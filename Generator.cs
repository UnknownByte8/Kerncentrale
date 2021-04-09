﻿namespace Kerncentrale
{
    class Generator
    {
        private double kWh;
        private double totalKWh;
        public double stoom;
        public double TotalKWh { get => totalKWh; set => totalKWh = value; }

        public double GetKWh()
        {
            return kWh;
        }

        /* 
         * <summary>
         * Sets current KWh
         *</summary>
         *<param name="value">Value to set KWh to</param>
         */
        private void SetKWh(double value)
        {
            kWh = value;
        }

        public void SetStoom(double value)
        {
            this.stoom = value;
        }

        public double GetStoom()
        {
            return this.stoom;
        }
        public Generator(int stoom)
        {
            SetStoom(stoom);
        }

        public Generator()
        {

        }

        /*
       * calculate how much energy is created
       */
        public void GenerateEnergy(double tmpStoom)
        {
            SetStoom(tmpStoom);
            double tmpKwh = (GetStoom() * 9.0);
            SetKWh(tmpKwh);

            this.TotalKWh += this.kWh;
        }
    }
}
