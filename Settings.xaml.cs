using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kerncentrale
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {

        public Settings()
        {
            this.InitializeComponent();
            this.ThreadTypeTextBlock.Text = "Current threadtype: " + (App.Current as App).threadingType.ToString();
        }

        /// <summary>
        /// Selection for selecting what type of threading will be used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var threadtypeComboBox = (ComboBox)sender;
            var selectedType = threadtypeComboBox.SelectedItem;

            switch (selectedType.ToString().ToLower())
            {
                case "threadpool":
                    UpdateThreadingType(ThreadingType.ThreadPool);
                    break;
                case "multithreading":
                    UpdateThreadingType(ThreadingType.MultiThreading);
                    break;
                case "singlethreading":
                    UpdateThreadingType(ThreadingType.SingleThreading);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Navigate to Main page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(MainPage), sender);
        }

        /// <summary>
        /// Updates what threading type will be used
        /// </summary>
        /// <param name="type"></param>
        private void UpdateThreadingType(ThreadingType type)
        {
            (App.Current as App).threadingType = type;
        }
    }
}
