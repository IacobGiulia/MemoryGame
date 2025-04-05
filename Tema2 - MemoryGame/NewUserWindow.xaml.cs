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
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        public event EventHandler UserAdded;
        public NewUserWindow()
        {
            InitializeComponent();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {

            UserAdded?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
    }
}
