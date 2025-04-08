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
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private GameViewModel _viewModel;
        private UserModel currentUser;
        public GameWindow(UserModel currentUser)
        {
            InitializeComponent();
            _viewModel = new GameViewModel(currentUser);
            this.DataContext = _viewModel;
        }

        //private void Card_Click(object sender, MouseButtonEventArgs e)
        //{
        //    var clickedImage = sender as Image;
        //    if (clickedImage != null)
        //    {
        //        var item = clickedImage.DataContext as CardModel;
        //        int index = _viewModel.Cards.IndexOf(item); // Obținem indexul din lista de carduri
        //        _viewModel.OnCardClicked(index); // Actualizăm jocul
        //    }
        //}

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.StartNewGame(); // Pornește un joc nou
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            PlayWindow playWindow = new PlayWindow(currentUser);
            playWindow.Show();
            this.Close();
        }
    }
}
