using System;
using System.Windows;

namespace FootballWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var fileName = Properties.Settings.Default.SourceFileName;

                ManageTeam manageTeam = new ManageTeam(fileName);
                if (manageTeam.IsValidFormat)
                {
                    if (manageTeam.IsValidData)
                    {
                        dgFootballGrid.ItemsSource = manageTeam.FootBallTeamList;
                        FootBallTeam footBallTeam = manageTeam.GetTeamWithMinDiff();
                        txtDiff.Text = "'" + footBallTeam.Name + "' with '" + footBallTeam.Diff + "' goal/s";
                    }
                    else
                        SetError("No Data found");
                }
                else
                    SetError("Invalid data format.");
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void SetError(string error)
        {
            txtTitle.Text = error;
            txtDiff.Text = string.Empty;
        }
    }


}
