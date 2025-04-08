using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace Tema2___MemoryGame
{
    class StatisticsViewModel : MainViewModel
    {
        private const string UsersFile = "users.json";

        public ObservableCollection<UserModel> Users { get; set; } = new();

        public StatisticsViewModel()
        {
            LoadStats();
        }

        private void LoadStats()
        {
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

    }
}
