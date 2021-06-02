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

            TotalReactors.Text = "Reactors present: " + (kerncentrale.GetReactors().Count);
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
                UpdateTemperatureLabels();

            }
            catch (Exception e)
            {
                Debug.WriteLine("Error while updating labels.\n", e.Message);

            }
        }
        /// <summary>
        /// This wil change waterlabels 
        /// </summary>
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

        /// <summary>
        /// Update Temperature labels
        /// </summary>
        private void UpdateTemperatureLabels()
        {

            double value1 = (kerncentrale.GetReactors()[reactorOffset].GetTemperatureFuelRods(1) / kerncentrale.GetReactors()[reactorOffset].GetOverheatTemperatureFuelRods()) * 100;
            double value2 = (kerncentrale.GetReactors()[reactorOffset + 1].GetTemperatureFuelRods(1) / kerncentrale.GetReactors()[reactorOffset + 1].GetOverheatTemperatureFuelRods()) * 100;
            double value3 = (kerncentrale.GetReactors()[reactorOffset + 2].GetTemperatureFuelRods(1) / kerncentrale.GetReactors()[reactorOffset + 2].GetOverheatTemperatureFuelRods()) * 100;
            if (value1 < 0)
            {
                value1 = 0;
            }
            if (value2 < 0)
            {
                value2 = 0;
            }
            if (value3 < 0)
            {
                value3 = 0;
            }
            //Temperatuur value1
            UpdateValuesDispatch(value: value1,
                                 progressbarControl: Temperature);
            //Temperatuur text1
            UpdateValuesDispatch(value: value1,
                                 textBlock: TemperatureText,
                                 Inhoudsmaat: "%",
                                 inhoud: "Danger level: ");
            //Temperatuur value2
            UpdateValuesDispatch(value: value2,
                                 progressbarControl: Temperature2);
            //Temperatuur text2
            UpdateValuesDispatch(value: value2,
                                 textBlock: TemperatureText2,
                                 Inhoudsmaat: "%",
                                 inhoud: "Danger level: ");
            //Temperatuur value3
            UpdateValuesDispatch(value: value3,
                                 progressbarControl: Temperature3);
            //Temperatuur text3
            UpdateValuesDispatch(value: value3,
                                 textBlock: TemperatureText3,
                                 Inhoudsmaat: "%",
                                 inhoud: "Danger level: ");
        }

        /// <summary>
        /// Updates labels on the UI
        /// </summary>
        /// <param name="value"></param>
        /// <param name="progressbarControl"></param>
        /// <param name="textBlock"></param>
        /// <param name="Inhoudsmaat"></param>
        /// <param name="inhoud"></param>
        private async void UpdateValuesDispatch(double value, Control progressbarControl = null, TextBlock textBlock = null, String Inhoudsmaat = null, String inhoud = null)
        {
            if (progressbarControl != null)
            {
                if (progressbarControl.GetType() == typeof(ProgressBar))
                {
                    await (progressbarControl as ProgressBar).Dispatcher.TryRunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        (progressbarControl as ProgressBar).Value = value;

                    });
                }
            }
            else if (textBlock != null)
            {
                if (textBlock.GetType() == typeof(TextBlock))
                {
                    await textBlock.Dispatcher.TryRunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        double t = Math.Round(value, 2);
                        textBlock.Text = inhoud + t.ToString() + Inhoudsmaat;
                    });


                }
            }

        }

        /// <summary>
        /// This wil change energylabels 
        /// </summary>
        private void UpdateEnergyLabels()
        {
            if ((reactorOffset + 2) < 5)
            {
                try
                {
                    //energy value1
                    UpdateValuesDispatch(value: kerncentrale.GetReactors()[reactorOffset].GetEnergy(),
                                         progressbarControl: Energy);
                    //energy text1
                    UpdateValuesDispatch(value: Energy.Value,
                                         textBlock: EnergyText,
                                         Inhoudsmaat: "kWh",
                                         inhoud: "Energie: ");
                    //energy value2
                    UpdateValuesDispatch(value: kerncentrale.GetReactors()[reactorOffset + 1].GetEnergy(),
                                         progressbarControl: Energy2);
                    //energy text2
                    UpdateValuesDispatch(value: Energy2.Value,
                                         textBlock: EnergyText2,
                                         Inhoudsmaat: "kWh",
                                         inhoud: "Energie: ");
                    //energy value3
                    UpdateValuesDispatch(value: kerncentrale.GetReactors()[reactorOffset + 2].GetEnergy(),
                                         progressbarControl: Energy3);
                    //energy text3
                    UpdateValuesDispatch(value: Energy3.Value,
                                         textBlock: EnergyText3,
                                         Inhoudsmaat: "kWh",
                                         inhoud: "Energie: ");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Exception occurred during updating of energy labels!\n", e.Message);
                }
            }

        }

        /// <summary>
        /// This wil change namelabels 
        /// </summary>
        /// <param name="id"></param>
        private void UpdateNameLabels(int id)
        {
            NameLabel.Text = "Reactor " + (id + 1);
            NameLabel2.Text = "Reactor " + (id + 2);
            NameLabel3.Text = "Reactor " + (id + 3);
        }

        /// <summary>
        /// This wil get executed to change labels 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="labelNumber"></param>
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
                int t = Int32.Parse(water);
                kerncentrale.GetReactors()[offset].KoelFuelrods(t);
            }
        }

        public void goMainPage()
        {
            Frame.Navigate(sourcePageType: typeof(MainPage));
        }
        #region reactor_buttons

        private void ReactorOffsetUp(object sender, RoutedEventArgs e)
        {
            if ((this.reactorOffset + 3) < kerncentrale.GetReactors().Count)
                this.reactorOffset++;
            UpdateWaterLabels();
            UpdateEnergyLabels();
            UpdateNameLabels(reactorOffset);
            UpdateTemperatureLabels();

        }
        private void ReactorOffsetDown(object sender, RoutedEventArgs e)
        {
            if (this.reactorOffset > 0)
                reactorOffset--;
            UpdateWaterLabels();
            UpdateEnergyLabels();
            UpdateNameLabels(reactorOffset);
            UpdateTemperatureLabels();
        }

        /// <summary>
        /// Water wordt toegevoegd aan de reactor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Exits the game and sets the highscore into the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ExitGame(object sender, RoutedEventArgs e)
        {
            double finalScore = Math.Round(kerncentrale.getTotalEnergy());
            DatabaseConnect.SetScore("" + DateTime.Now, finalScore.ToString());

            this.Frame.Navigate(typeof(MainPage), sender);
        }
    }
}
