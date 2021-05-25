using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static Kerncentrale.DatabaseConnect;

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
            //SetScore("gameTest", "66666666"); // TEST
            CreateList();
        }
 

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), sender);
        }

        private void CreateList()
        {
            int countUp = 0;
            List<ScoreInfo> allInfoScores = DatabaseConnect.GetScore();
            foreach(ScoreInfo score in allInfoScores)
            {
                countUp++;
                switch(countUp)
                {
                    case 1:
                        Score1.Content = "On " + score.Name + " you scored: " + score.PointsScored;
                        break;
                    case 2:
                        Score2.Content = "On " + score.Name + " you scored: " + score.PointsScored;
                        break;
                    case 3:
                        Score3.Content = "On " + score.Name + " you scored: " + score.PointsScored;
                        break;
                    case 4:
                        Score4.Content = "On " + score.Name + " you scored: " + score.PointsScored;
                        break;
                    case 5:
                        Score5.Content = "On " + score.Name + " you scored: " + score.PointsScored;
                        break;
                }                
            }
        }
    }
}
