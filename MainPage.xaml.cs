using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Kerncentrale
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ThreadingType threadingType;

        public ThreadingType ThreadingType { get => threadingType; set => threadingType = value; }

        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the current threading type
        /// </summary>
        /// <param name="type"></param>
        public void SetThreadType(ThreadingType type)
        {
            ThreadingType = type;
        }

        private void Highscore_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(sourcePageType: typeof(Highscore));
        }

        /// <summary>
        /// click on settings button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(sourcePageType: typeof(Settings));
        }

        /// <summary>
        /// click on play button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            InitDB();
            Frame.Navigate(sourcePageType: typeof(Game), null);
        }

        /// <summary>
        /// Creates/checks the db creation 
        /// </summary>
        private void InitDB()
        {
            DatabaseConnect.CreateDB();
        }

        /// <summary>
        /// Navigation
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //If action in EditPage occurred, determine that here
            switch ((Application.Current as App).threadingType)
            {
                case ThreadingType.MultiThreading:
                    SetThreadType(ThreadingType.MultiThreading);
                    break;
                case ThreadingType.SingleThreading:
                    SetThreadType(ThreadingType.SingleThreading);
                    break;
                case ThreadingType.ThreadPool:
                    SetThreadType(ThreadingType.ThreadPool);
                    break;
            }
        }
    }
}
