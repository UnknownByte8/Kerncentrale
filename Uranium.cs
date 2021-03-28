﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerncentrale
{
    //voorbeeld soort fuelrod
    class Uranium : FuelRod
    {
        private string name = "Uranium";
        private int huidigeTemperatuur = 0;
        private int overhittingstemperatuur = 5000;
        private int optimaleTemperatuur = 4000;
        private int onderLimietTemperatuur = 3000;
        private double graadPerLiter = 0.5;

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
