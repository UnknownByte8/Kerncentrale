using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Kerncentrale
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        Kerncentrale kerncentrale;
        int reactorOffset = 0;
        public Game()
        {

            this.InitializeComponent();
            WaterText.Text = Water.Value.ToString();
            EnergyText.Text = Energy.Value.ToString();
            TemperatureText.Text = Temperature.Value.ToString();

            kerncentrale = new Kerncentrale();
        }

        public void createButton(string name)
        {

        }

        public void createProgressbar(string name)
        {

        }
        private void reactorOffsetUp(object sender, RoutedEventArgs e)
        {
            if((this.reactorOffset+2) < kerncentrale.getReactors().Count)
                this.reactorOffset++;
        }
        private void reactorOffsetDown(object sender, RoutedEventArgs e)
        {
            if(this.reactorOffset > 1)
                reactorOffset--;
        }

        private void execute(int offset)
        {
            //stoom water electriciteit?
        }

        // Reactor 1
        // Water wordt toegevoegd aan de reactor
        private void WaterUp(object sender, RoutedEventArgs e)
        {
            
            Water.Value += 1;
            WaterText.Text = Water.Value.ToString();
            Debug.WriteLine(Water.Value);
            this.execute(reactorOffset);
        }

        private void WaterDown(object sender, RoutedEventArgs e)
        {
            Water.Value -= 1;
            WaterText.Text = Water.Value.ToString();
            this.execute(reactorOffset);

        }

        //private void EnergyUp(object sender, RoutedEventArgs e)
        //{
        //    Energy.Value += 1;
        //    EnergyText.Text = Energy.Value.ToString();
        //}

        //private void EnergyDown(object sender, RoutedEventArgs e)
        //{
        //    Energy.Value -= 1;
        //    EnergyText.Text = Energy.Value.ToString();
        //}

        //Reactor 2

        private void WaterUp2(object sender, RoutedEventArgs e)
        {

            Water2.Value += 1;
            WaterText2.Text = Water2.Value.ToString();
            Debug.WriteLine(Water.Value);
            this.execute(reactorOffset+1);
        }

        private void WaterDown2(object sender, RoutedEventArgs e)
        {
            Water2.Value -= 1;
            WaterText2.Text = Water2.Value.ToString();
            this.execute(reactorOffset+1);

        }

        //private void EnergyUp2(object sender, RoutedEventArgs e)
        //{
        //    Energy2.Value += 1;
        //    EnergyText2.Text = Energy2.Value.ToString();
        //}

        //private void EnergyDown2(object sender, RoutedEventArgs e)
        //{
        //    Energy2.Value -= 1;
        //    EnergyText2.Text = Energy2.Value.ToString();
        //}

        //Reactor 3

        private void WaterUp3(object sender, RoutedEventArgs e)
        {

            Water3.Value += 1;
            WaterText3.Text = Water3.Value.ToString();
            Debug.WriteLine(Water3.Value);
            this.execute(reactorOffset+2);
        }

        private void WaterDown3(object sender, RoutedEventArgs e)
        {
            Water3.Value -= 1;
            WaterText3.Text = Water3.Value.ToString();
            this.execute(reactorOffset+2);
        }

        //private void EnergyUp3(object sender, RoutedEventArgs e)
        //{
        //    Energy3.Value += 1;
        //    EnergyText3.Text = Energy3.Value.ToString();
        //}

        //private void EnergyDown3(object sender, RoutedEventArgs e)
        //{
        //    Energy3.Value -= 1;
        //    EnergyText3.Text = Energy3.Value.ToString();
        //}

    }
}
