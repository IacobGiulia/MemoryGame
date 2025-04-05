﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tema2___MemoryGame;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new SignInViewModel(this);
    }

    private void NewUserButton_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = this.DataContext as SignInViewModel;
        var newUserWindow = new NewUserWindow();
        newUserWindow.UserAdded += (s, args) =>
        {
            viewModel.LoadUsers(); // reîncarcă lista după ce se adaugă
        };
        newUserWindow.ShowDialog();
    }

}