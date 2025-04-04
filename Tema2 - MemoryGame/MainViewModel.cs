using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Tema2___MemoryGame
{
    class MainViewModel: INotifyPropertyChanged
    {
        public ICommand ExitCommand { get; }

        public MainViewModel()
        {
            ExitCommand = new RelayCommand(ExitApplication);
        }

        private void ExitApplication(object obj)
        {
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
