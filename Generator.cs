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
        private double totalKWh;
        public int Stoom { get; set; }
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
        }

        public Generator(int stoom)
        {
            Stoom = stoom;
        }

        public Generator()
        {

        }

        public void GenerateEnergy(double stoom)
        {
            SetKWh(Stoom * 9.0);

            TotalKWh += kWh;
        }
    }
}
