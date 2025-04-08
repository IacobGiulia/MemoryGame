using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tema2___MemoryGame
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    public partial class PlayWindow : Window
    {
        public UserModel CurrentUser { get; set; }
        public PlayWindow(UserModel currentUser)
        {
            InitializeComponent();
            CurrentUser = currentUser;
        }

        private void ExitOption_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void AboutOption_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Iacob Giulia - giulia.iacob@student.unitbv.ro, grupa 10LF331 - Informatica Aplicata", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NewGameOption_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verifică dacă CurrentUser este null
                if (CurrentUser == null)
                {
                    MessageBox.Show("CurrentUser este null!");
                    return;
                }

                
                GameWindow gameWindow = new GameWindow(CurrentUser);
               
                gameWindow.Show();
                
                this.Close();
            }
            catch (Exception ex)
            {
                // Afișează informații detaliate despre excepție
                MessageBox.Show($"Error: {ex.Message}\n\nStack Trace: {ex.StackTrace}");
            }
        }
        private void StatisticsOption_Click(object sender, RoutedEventArgs e)
        {
            var statisticsWindow = new Statistics();
            statisticsWindow.ShowDialog();
        }
    }
    
}
