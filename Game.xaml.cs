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
        public Game()
        {

            this.InitializeComponent();
            WaterText.Text = Water.Value.ToString();
            EnergyText.Text = Energy.Value.ToString();
            TemperatureText.Text = Temperature.Value.ToString();
        }

        public void createButton(string name)
        {

        }

        public void createProgressbar(string name)
        {

        }
        // Reactor 1
        // Water wordt toegevoegd aan de reactor
        private void WaterUp(object sender, RoutedEventArgs e)
        {
            
            Water.Value += 1;
            WaterText.Text = Water.Value.ToString();
            Debug.WriteLine(Water.Value);
        }

        private void WaterDown(object sender, RoutedEventArgs e)
        {
            Water.Value -= 1;
            WaterText.Text = Water.Value.ToString();

        }

        private void EnergyUp(object sender, RoutedEventArgs e)
        {
            Energy.Value += 1;
            EnergyText.Text = Energy.Value.ToString();
        }

        private void EnergyDown(object sender, RoutedEventArgs e)
        {
            Energy.Value -= 1;
            EnergyText.Text = Energy.Value.ToString();
        }

        //Reactor 2

        private void WaterUp2(object sender, RoutedEventArgs e)
        {

            Water2.Value += 1;
            WaterText2.Text = Water2.Value.ToString();
            Debug.WriteLine(Water.Value);
        }

        private void WaterDown2(object sender, RoutedEventArgs e)
        {
            Water2.Value -= 1;
            WaterText2.Text = Water2.Value.ToString();

        }

        private void EnergyUp2(object sender, RoutedEventArgs e)
        {
            Energy2.Value += 1;
            EnergyText2.Text = Energy2.Value.ToString();
        }

        private void EnergyDown2(object sender, RoutedEventArgs e)
        {
            Energy2.Value -= 1;
            EnergyText2.Text = Energy2.Value.ToString();
        }

        //Reactor 3

        private void WaterUp3(object sender, RoutedEventArgs e)
        {

            Water3.Value += 1;
            WaterText3.Text = Water3.Value.ToString();
            Debug.WriteLine(Water3.Value);
        }

        private void WaterDown3(object sender, RoutedEventArgs e)
        {
            Water3.Value -= 1;
            WaterText3.Text = Water3.Value.ToString();

        }

        private void EnergyUp3(object sender, RoutedEventArgs e)
        {
            Energy3.Value += 1;
            EnergyText3.Text = Energy3.Value.ToString();
        }

        private void EnergyDown3(object sender, RoutedEventArgs e)
        {
            Energy3.Value -= 1;
            EnergyText3.Text = Energy3.Value.ToString();
        }

    }
}
