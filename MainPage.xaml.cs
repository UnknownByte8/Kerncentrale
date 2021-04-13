


﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Kerncentrale
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        /*
         * click on settings button
         */
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings));
        }

        /*
         * click on play button
         */
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Game));
            this.InitDB();
        }

        private void InitDB()
        {
            DatabaseConnect.CreateDB();
            //userView.ItemsSource = DatabaseConnect.GetRecords();
            //userView2.ItemsSource = DatabaseConnect.GetHighscore();

        }
    }
}
