using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerncentrale
{
    //voorbeeld soort fuelrod
    class Plutonium : FuelRod
    {
        private string name = "Plutonium";
        private int huidigeTemperatuur = 0;
        private int overhittingstemperatuur = 2500;
        private int optimaleTemperatuur = 2000;
        private int onderLimietTemperatuur = 1500;
        private double graadPerLiter = 1.5;

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
