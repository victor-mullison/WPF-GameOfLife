using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFLife
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ResetButtonPressed(object sender, RoutedEventArgs e)
        {
            LifeCanvas.ResetGrid();
            PlayPauseIcon.Source = playIcon;

            if (LifeCanvas.IsPlaying())
            {
                LifeCanvas.PlayOrPause();
            }
        }
        private void RandomButtonPressed(object sender, RoutedEventArgs e)
        {
            LifeCanvas.RandomizeGrid();
            PlayPauseIcon.Source = playIcon;

            if (LifeCanvas.IsPlaying())
            {
                LifeCanvas.PlayOrPause();
            }
        }

        private BitmapImage playIcon = new(new Uri("Images/play.png", UriKind.Relative));
        private BitmapImage pauseIcon = new(new Uri("Images/pause.png", UriKind.Relative));

        private void PlayButtonPressed(object sender, RoutedEventArgs e)
        {
            if (LifeCanvas.IsPlaying()) // Pausing, set image resource to play button
            {
                PlayPauseIcon.Source = playIcon;
            }
            else // Set to pause button
            {
                PlayPauseIcon.Source = pauseIcon;
            }

            LifeCanvas.PlayOrPause();
        }

        private void SimSpeedSlider_Changed(object sender, RoutedEventArgs e)
        {
            // Sim slider has range (1 - 5), starts at 3, which is normal speed (100ms).
            // 1 and 2 should quarter and halve the speed, respectively.
            // 4 and 5 should double and quadruple.
            int newval = (int)(100 * Math.Pow(2, 3 - (int)SimSpeedSlider.Value));

            float viewFloat = (float)(1 / Math.Pow(2, 3 - SimSpeedSlider.Value)); // Obviously.
            if(SimSpeedView != null) // To prevent null exception on startup (before the view is initialized)
            {
                SimSpeedView.Text = viewFloat.ToString() + "x";
            }
            
            LifeCanvas.SIMSPEED = newval;

        }

        private void GameSizeSlider_Changed(object sender, RoutedEventArgs e)
        {
            if (GridSizeView == null)// To prevent null exception on startup (before the view is initialized)
            {
                return;
            }

            if (LifeCanvas.IsPlaying())
            {
                LifeCanvas.PlayOrPause();
            }
            PlayPauseIcon.Source = playIcon;

            // Preset sizes (must divide 400) are 10, 20, 40, 50, 80
            // Not gonna do fancy math. I already broke my brain with the sim speed slider.
            switch ((int)GameSizeSlider.Value)
            {
                case 1:
                    GridSizeView.Text = "10x10";
                    LifeCanvas.GRIDSIZE = 10;
                    break;
                case 2:
                    GridSizeView.Text = "20x20";
                    LifeCanvas.GRIDSIZE = 20;
                    break;
                case 3:
                    GridSizeView.Text = "40x40";
                    LifeCanvas.GRIDSIZE = 40;
                    break;
                case 4:
                    GridSizeView.Text = "50x50";
                    LifeCanvas.GRIDSIZE = 50;
                    break;
                case 5:
                    GridSizeView.Text = "80x80";
                    LifeCanvas.GRIDSIZE = 80;
                    break;
            }

            LifeCanvas.RandomizeGrid();
        }
    }
}