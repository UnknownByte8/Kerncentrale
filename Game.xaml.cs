using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Kerncentrale
{

    public sealed partial class Game : Page
    {
        private static ThreadingType threadingType;
        private Kerncentrale kerncentrale;
        private int reactorOffset = 0;
        BackgroundWorker bgWorker;
        

        public ThreadingType ThreadingType { get => threadingType; set => threadingType = value; }
        public bool EnergyLabelInUse { get; set; } = false;

        public Game()
        {
            this.InitializeComponent();
            bgWorker = new BackgroundWorker();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ThreadingType = (App.Current as App).threadingType;
            kerncentrale = new Kerncentrale(threadingType: threadingType);

            UpdateLabels();

            // wacht 5 seconden om de UI in te laten laden, voer dan de functie uit.
            Task.Delay(TimeSpan.FromSeconds(5));

            ExecuteThreads();
        }
        /// <summary>
        /// Sets & executes a background worker
        /// </summary>
        private void ExecuteThreads()
        {
            try
            {
                bgWorker.DoWork += new DoWorkEventHandler(BgWorker_do_work);
                bgWorker.ProgressChanged += new ProgressChangedEventHandler
                        (BgWorker_ProgressChanged);
                bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                        (BgWorker_RunWorkerCompleted);
                bgWorker.WorkerReportsProgress = true;
                bgWorker.WorkerSupportsCancellation = true;

                bgWorker.RunWorkerAsync();

            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception occurred!\n", e.Message);
            }
            
        }

        /// <summary>
        /// background worker method
        /// Exectute methods that have nothing to do with the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BgWorker_do_work(object sender, DoWorkEventArgs e)
        {
            kerncentrale.GenerateThreads();
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5);
                bgWorker.ReportProgress(i);

                if (bgWorker.CancellationPending)
                {
                    // Set the e.Cancel flag so that the WorkerCompleted event
                    // knows that the process was cancelled.
                    e.Cancel = true;
                    bgWorker.ReportProgress(0);
                    return;
                }
            }

            bgWorker.ReportProgress(100);
        }

        void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Debug.WriteLine("Worker progress: {0}\n", e.ProgressPercentage);
            switch (e.ProgressPercentage)
            {
                case 10:
                case 40:
                case 80:
                    UpdateLabels();
                    break;
                default:
                    break;
            }
            UpdateLabels();

        }

        void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) 
        {
            if (e.Cancelled)
            {
                Debug.WriteLine("Worker was canceled\n");

            }
            // Check to see if an error occurred in the background process.
            else if (e.Error != null)
            {
                Debug.WriteLine("Error while performing background operation.\n", e.Error);
            }
            else
            {
                Debug.WriteLine("Task Completed...");
            }
            //update the labels with updated values
            UpdateLabels();
            //run bgworker again when it finished.
            bgWorker.RunWorkerAsync();

        }

        private void UpdateLabels()
        {
            try
            {
                UpdateEnergyLabels();
                UpdateWaterLabels();
                UpdateNameLabels(reactorOffset);
                WaterText.Text = Water.Value.ToString();
                EnergyText.Text = Energy.Value.ToString();
                TemperatureText.Text = Temperature.Value.ToString();
                
                
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error while updating labels.\n", e.Message);

            }
        }
        /*
       * This wil change waterlabels 
       */
        private void UpdateWaterLabels()
        {
            try
            {
                if ((reactorOffset + 2) < 5)
                {
                    Water.Value = kerncentrale.GetReactors()[reactorOffset].GetWaterFuelRods();
                    WaterText.Text = Water.Value.ToString();
                    Water2.Value = kerncentrale.GetReactors()[reactorOffset + 1].GetWaterFuelRods();
                    WaterText2.Text = Water2.Value.ToString();
                    Water3.Value = kerncentrale.GetReactors()[reactorOffset + 2].GetWaterFuelRods();
                    WaterText3.Text = Water3.Value.ToString();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception occurred during updating of water labels!\n", e.Message);
            }
            
        }

        private void UpdateTemperatureLabels()
        {
            //energy value
            UpdateValuesDispatch(value: kerncentrale.GetReactors()[reactorOffset].GetEnergy(),
                                 progressbarControl: Temperature);
            //energy text
            UpdateValuesDispatch(value: Energy.Value,
                                 textBlock: TemperatureText);
            //energy value
            UpdateValuesDispatch(value: kerncentrale.GetReactors()[reactorOffset + 1].GetEnergy(),
                                 progressbarControl: Temperature2);
            //energy text
            UpdateValuesDispatch(value: Energy2.Value,
                                 textBlock: TemperatureText2);
            //energy value
            UpdateValuesDispatch(value: kerncentrale.GetReactors()[reactorOffset + 2].GetEnergy(),
                                 progressbarControl: Temperature3);
            //energy text
            UpdateValuesDispatch(value: Energy3.Value,
                                 textBlock: TemperatureText3);
        }

        private async void UpdateValuesDispatch(double value, Control progressbarControl = null, TextBlock textBlock = null)
        {
            if (progressbarControl != null)
            {
                if (progressbarControl.GetType() == typeof(ProgressBar))
                {
                    //ProgressBar progressbar = (ProgressBar)progressbarControl;
                    /*await (progressbarControl as ProgressBar).Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                        (progressbarControl as ProgressBar).Value = value;

                    });*/
                    await (progressbarControl as ProgressBar).Dispatcher.TryRunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                        (progressbarControl as ProgressBar).Value = value;

                    });
                }
            }
            else if (textBlock != null)
            {
                if (textBlock.GetType() == typeof(TextBlock))
                {
                    await textBlock.Dispatcher.TryRunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                        //control. = value;
                        textBlock.Text = value.ToString();
                    });

                }
            }

        }

        /*
          * This wil change energylabels 
        */
        private void UpdateEnergyLabels()
        {
            if ((reactorOffset + 2) < 5)
            {
                try
                {
                    //energy value
                    UpdateValuesDispatch(value: kerncentrale.GetReactors()[reactorOffset].GetEnergy(),
                                         progressbarControl: Energy);
                    //energy text
                    UpdateValuesDispatch(value: Energy.Value,
                                         textBlock: EnergyText);
                    //energy value
                    UpdateValuesDispatch(value: kerncentrale.GetReactors()[reactorOffset + 1].GetEnergy(),
                                         progressbarControl: Energy2);
                    //energy text
                    UpdateValuesDispatch(value: Energy2.Value,
                                         textBlock: EnergyText2);
                    //energy value
                    UpdateValuesDispatch(value: kerncentrale.GetReactors()[reactorOffset + 2].GetEnergy(),
                                         progressbarControl: Energy3);
                    //energy text
                    UpdateValuesDispatch(value: Energy3.Value,
                                         textBlock: EnergyText3);

                    /* Energy.Value = kerncentrale.GetReactors()[reactorOffset].getEnergy();
                     EnergyText.Text = Energy.Value.ToString();
                     Energy2.Value = kerncentrale.GetReactors()[reactorOffset + 1].getEnergy();
                     EnergyText2.Text = Energy2.Value.ToString();
                     Energy3.Value = kerncentrale.GetReactors()[reactorOffset + 2].getEnergy();
                     EnergyText3.Text = Energy3.Value.ToString();*/

                }
                catch (Exception e)
                {
                    Debug.WriteLine("Exception occurred during updating of energy labels!\n", e.Message);
                }
            }

            /*Thread thread = new Thread(this.UpdateEnergyLabels);
            thread.Name = "Update Energy Labels ";
            //Thread.Sleep(1000);
            thread.Start();*/
        }

        private void UpdateNameLabels(int id)
        {
            NameLabel.Text = "Reactor " + (id + 1);
            NameLabel2.Text = "Reactor " + (id + 2);
            NameLabel3.Text = "Reactor " + (id + 3);
        }

        /*
        * This wil get executed to change labels 
        */
        private void Execute(int offset, int labelNumber)
        {
            if (offset < 5)
            {
                string water = "";
                switch (labelNumber)
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
                kerncentrale.GetReactors()[offset].KoelFuelrods(Int32.Parse(water));
            }
        }
        #region reactor_buttons

        private void ReactorOffsetUp(object sender, RoutedEventArgs e)
        {
            if ((this.reactorOffset + 2) < kerncentrale.GetReactors().Count)
                this.reactorOffset++;
            UpdateWaterLabels();
            UpdateEnergyLabels();
            UpdateNameLabels(reactorOffset);

        }
        private void ReactorOffsetDown(object sender, RoutedEventArgs e)
        {
            if (this.reactorOffset > 0)
                reactorOffset--;
            UpdateWaterLabels();
            UpdateEnergyLabels();
            UpdateNameLabels(reactorOffset);
        }

        // Reactor 1
        // Water wordt toegevoegd aan de reactor
        private void WaterUp(object sender, RoutedEventArgs e)
        {
            Water.Value += 1;
            WaterText.Text = Water.Value.ToString();
            Debug.WriteLine(Water.Value);
            this.Execute(reactorOffset, 1);
        }

        private void WaterDown(object sender, RoutedEventArgs e)
        {
            Water.Value -= 1;
            WaterText.Text = Water.Value.ToString();
            this.Execute(reactorOffset, 1);
        }

        //Reactor 2
        private void WaterUp2(object sender, RoutedEventArgs e)
        {
            Water2.Value += 1;
            WaterText2.Text = Water2.Value.ToString();
            Debug.WriteLine(Water.Value);
            this.Execute(reactorOffset + 1, 2);
        }

        private void WaterDown2(object sender, RoutedEventArgs e)
        {
            Water2.Value -= 1;
            WaterText2.Text = Water2.Value.ToString();
            this.Execute(reactorOffset + 1, 2);
        }

        //Reactor 3
        private void WaterUp3(object sender, RoutedEventArgs e)
        {
            Water3.Value += 1;
            WaterText3.Text = Water3.Value.ToString();
            Debug.WriteLine(Water3.Value);
            this.Execute(reactorOffset + 2, 3);
        }

        private void WaterDown3(object sender, RoutedEventArgs e)
        {
            Water3.Value -= 1;
            WaterText3.Text = Water3.Value.ToString();
            this.Execute(reactorOffset + 2, 3);
        }
        #endregion
    }
}
