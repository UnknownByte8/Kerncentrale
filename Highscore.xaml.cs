using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kerncentrale
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Highscore : Page
    {

        public Highscore()
        {
            this.InitializeComponent();
        }
 

        private void Return_Click(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(MainPage), sender);
        }
    }
}
