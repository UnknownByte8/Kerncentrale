using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerncentrale
{
    //voorbeeld soort fuelrod
    class Uranium : FuelRod
    {
        private string name;
        private int huidigeTemperatuur;
        private int overhittingstemperatuur;
        private int optimaleTemperatuur;
        private int onderLimietTemperatuur;
        private double graadPerLiter;

        public bool Execute()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return name;
        }

        public int getHuidigeTemperature(){
            return huidigeTemperatuur;
        }

        public int getOnderLimietTemperatuur(){
            return onderLimietTemperatuur;
        }

        public int getOptimaleTemperatuur(){
            return optimaleTemperatuur;
        }

        public int getOverhittingsTemperatuur(){
            return overhittingstemperatuur;
        }

        public double getGraadPerLiter(){
            return graadPerLiter;
        }

        public void setTemperature(int newTemprature){
            huidigeTemperatuur = newTemprature;
        }
    }
}
