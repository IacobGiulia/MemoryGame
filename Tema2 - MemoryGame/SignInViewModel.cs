using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tema2___MemoryGame
{
    public class SignInViewModel : MainViewModel
    {
        private const string UsersFile = "users.json";
        public ObservableCollection<UserModel> Users { get; set; } = new();
        public UserModel SelectedUser { get; set; }

        public ICommand AddUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand PlayCommand { get; }

        public ICommand ExitGameCommand { get; }
        public ICommand NewUserCommand { get; }

        public SignInViewModel()
        {
            LoadUsers();

            //AddUserCommand = new RelayCommand(AddUser);
            DeleteUserCommand = new RelayCommand(DeleteUser, CanModifyUser);
            PlayCommand = new RelayCommand(Play, CanModifyUser);
            ExitGameCommand = new RelayCommand(ExitApp);
            NewUserCommand = new RelayCommand(_ => OpenNewUserWindow());

        }

        private void OpenNewUserWindow()
        {
            var window = new NewUserWindow();
            window.UserAdded += (s, e) => LoadUsers(); // 🟢 ADĂUGĂ AICI
            window.DataContext = new NewUserViewModel(window);
            window.ShowDialog();
        }
        public void LoadUsers()
        {
            Users.Clear();
            if (File.Exists(UsersFile))
            {
                var json = File.ReadAllText(UsersFile);
                var userList = JsonSerializer.Deserialize<List<UserModel>>(json);
                if (userList != null)
                {
                    foreach (var user in userList)
                    {
                        Users.Add(user);
                    }
                }
            }
        }

        private void SaveUsers()
        {
            var json = JsonSerializer.Serialize(Users);
            File.WriteAllText(UsersFile, json);
        }

        //private void AddUser(object obj)
        //{
        //    OpenFileDialog openFileDialog = new()
        //    {
        //        Filter = "Image Files(*.jpg, *.png, *.gif) | *.jpg;*.png;*.gif",
        //        Title = "Select an Image"
        //    };

        //    if (openFileDialog.ShowDialog() == true)
        //    {
        //        string fileName = Path.GetFileName(openFileDialog.FileName);
        //        string newPath = Path.Combine("Images", fileName);

        //        if (!Directory.Exists("Images"))
        //            Directory.CreateDirectory("Images");

        //        File.Copy(openFileDialog.FileName, newPath, true);

        //        UserModel newUser = new() { Username = $"User{Users.Count + 1}", ImagePath = newPath };
        //        Users.Add(newUser);
        //        SaveUsers();
        //    }



        //}

        private void DeleteUser(object obj)
        {
            if (SelectedUser != null)
            {
                Users.Remove(SelectedUser);
                SaveUsers();
            }
        }

        private void Play(object obj)
        {
            MessageBox.Show($"User {SelectedUser.Username} is playing!");
        }

        private void ExitApp(object obj)
        {
            Application.Current.Shutdown();
        }

        private bool CanModifyUser(object obj) => SelectedUser != null;
    }
}