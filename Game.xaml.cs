using System;
using System.Diagnostics;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kerncentrale
{
     public sealed partial class Game : Page
    {
        Kerncentrale kerncentrale = new Kerncentrale();
        int reactorOffset = 0;

        public Game()
        {
   
            this.InitializeComponent();

            updateWaterLabels();
            updateEnergyLabels();
            updateNameLabels(reactorOffset);
            

            WaterText.Text = Water.Value.ToString();
            EnergyText.Text = Energy.Value.ToString();
            TemperatureText.Text = Temperature.Value.ToString();

        }


        private void continuousThread()
        {
            Thread th = new Thread(() =>
            {
                while (true)
                {
                    //call getEnergy for given reactor & update labels
                    updateEnergyLabels();


                    //call executethread in given reactor
                    //why?

                    //kerncentrale.getReactors()[reactorOffset].executeThread();

                }
            });

            th.Start();
        }

        private void updateWaterLabels()
        {
            if ((reactorOffset + 2) < 20 )
            {
                Water.Value = kerncentrale.getReactors()[reactorOffset].getWaterFuelRods();
                WaterText.Text = Water.Value.ToString();
                Water2.Value = kerncentrale.getReactors()[reactorOffset + 1].getWaterFuelRods();
                WaterText2.Text = Water2.Value.ToString();
                Water3.Value = kerncentrale.getReactors()[reactorOffset + 2].getWaterFuelRods();
                WaterText3.Text = Water3.Value.ToString();
            }

        }
        private void updateEnergyLabels()
        {
            if ((reactorOffset + 2) < 20)
            {
                Energy.Value = kerncentrale.getReactors()[reactorOffset].getEnergy();
                EnergyText.Text = Energy.Value.ToString();
                Energy2.Value = kerncentrale.getReactors()[reactorOffset + 1].getEnergy();
                EnergyText2.Text = Energy2.Value.ToString();
                Energy3.Value = kerncentrale.getReactors()[reactorOffset + 2].getEnergy();
                EnergyText3.Text = Energy3.Value.ToString();
            }
        }

        private void updateNameLabels(int id)
        {
            NameLabel.Text = "Reactor " + (id+1);
            NameLabel2.Text = "Reactor " + (id+2);
            NameLabel3.Text = "Reactor " + (id+3);
        }
        private void execute(int offset, int labelNumber)
        {
            if(offset < 20) { 
            string water = ""; 
            switch(labelNumber)
            {
                case 1:
                    water = WaterText.Text;
                    break;
                case 2:
                    water = WaterText2.Text;
                    break;
                case 3:
                    water = WaterText3.Text;
                    break;
            }
            //stoom water electriciteit?
            kerncentrale.getReactors()[offset].koelFuelrods(Int32.Parse(water));
            }
        }
        #region reactor_buttons

        private void reactorOffsetUp(object sender, RoutedEventArgs e)
        {
            if ((this.reactorOffset + 2) < kerncentrale.getReactors().Count)
                this.reactorOffset++;
            updateWaterLabels();
            updateEnergyLabels();
            updateNameLabels(reactorOffset);

        }
        private void reactorOffsetDown(object sender, RoutedEventArgs e)
        {
            if (this.reactorOffset > 0)
                reactorOffset--;
            updateWaterLabels();
            updateEnergyLabels();
            updateNameLabels(reactorOffset);

        }

        // Reactor 1
        // Water wordt toegevoegd aan de reactor
        private void WaterUp(object sender, RoutedEventArgs e)
        {
            Water.Value += 1;
            WaterText.Text = Water.Value.ToString();
            Debug.WriteLine(Water.Value);
            this.execute(reactorOffset, 1);
        }

        private void WaterDown(object sender, RoutedEventArgs e)
        {
            Water.Value -= 1;
            WaterText.Text = Water.Value.ToString();
            this.execute(reactorOffset, 1);

        }

        //Reactor 2

        private void WaterUp2(object sender, RoutedEventArgs e)
        {

            Water2.Value += 1;
            WaterText2.Text = Water2.Value.ToString();
            Debug.WriteLine(Water.Value);
            this.execute(reactorOffset+1, 2);
        }

        private void WaterDown2(object sender, RoutedEventArgs e)
        {
            Water2.Value -= 1;
            WaterText2.Text = Water2.Value.ToString();
            this.execute(reactorOffset+1, 2);

        }

        //Reactor 3

        private void WaterUp3(object sender, RoutedEventArgs e)
        {

            Water3.Value += 1;
            WaterText3.Text = Water3.Value.ToString();
            Debug.WriteLine(Water3.Value);
            this.execute(reactorOffset+2, 3);
        }

        private void WaterDown3(object sender, RoutedEventArgs e)
        {
            Water3.Value -= 1;
            WaterText3.Text = Water3.Value.ToString();
            this.execute(reactorOffset+2, 3);
        }

        public void backToMenu()
        {
            Debug.WriteLine("GA UIT");
          //  this.Frame.Navigate(typeof(MainPage));
        }
        #endregion

    }
}
