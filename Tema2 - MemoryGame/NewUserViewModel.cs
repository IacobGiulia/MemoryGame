using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.IO;

namespace Tema2___MemoryGame
{
    class NewUserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler UserAdded;
        private string _username;
        private string _imagePath;


        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        public ICommand ChooseImageCommand { get; }
        public ICommand SaveUserCommand { get; }

        private Window _window;

        public NewUserViewModel(Window window)
        {
            _window = window;
            ChooseImageCommand = new RelayCommand(_ => ChooseImage());
            SaveUserCommand = new RelayCommand(_ => SaveUser(), _ => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(ImagePath));
        }

        private void ChooseImage()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.png;*.gif",
                Title = "Select Profile Image"
            };

            if (dialog.ShowDialog() == true)
            {
                ImagePath = dialog.FileName;
            }
        }

        private void SaveUser()
        {
            try
            {
                var user = new UserModel { Username = Username, ImagePath = ImagePath };
                var usersFile = "users.json";
                var userList = new List<UserModel>();

                if (File.Exists(usersFile))
                {
                    var json = File.ReadAllText(usersFile);
                    userList = JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();
                }

                userList.Add(user);
                var serialized = JsonSerializer.Serialize(userList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(usersFile, serialized);

                MessageBox.Show("User created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                UserAdded?.Invoke(this, EventArgs.Empty);

                _window.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving user: " + ex.Message);
            }
        }

        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

