using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerncentrale
{
    class Generator
    {
        private double kWh;

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
        }

        private double totalKWh;

        public double GetTotalKWh()
        {
            return totalKWh;
        }

        private void SetTotalKWh(double value)
        {
            totalKWh = value;
        }

        public int Stoom { get; set; }

        public Generator(int stoom)
        {
            Stoom = stoom;
        }

        public Generator()
        {

        }

        public void GenerateKWH()
        {
            SetKWh(Stoom * 9.0);

        }
    }
}
