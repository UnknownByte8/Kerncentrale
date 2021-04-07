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
            TemperatureText.Text = Temperature.Value.ToString();
        }

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


    }
}
