using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Tema2___MemoryGame
{
    public class GameViewModel: MainViewModel
    {
        private GameModel _gameModel;
        private DispatcherTimer _timer;
        private UserModel _currentUser;

        private string _gameStatus;

        public ICommand CardClickCommand { get; private set; }
        public ICommand NewGameCommand { get; private set; }

        public string TimerText => $"Time Left: {_gameModel.TimeLeft}s";

        public ObservableCollection<CardModel> Cards => _gameModel.Cards;

        public bool IsGameOver => _gameModel.IsGameOver;

        public string GameStatus
        {
            get => _gameStatus;
            set
            {
                _gameStatus = value;
                OnPropertyChanged(nameof(GameStatus));
            }
        }

        public GameViewModel(UserModel currentUser)
        {

            _currentUser = currentUser;
            _gameModel = new GameModel();
            _gameModel.GameUpdated += OnGameUpdated;

            CardClickCommand = new RelayCommand(OnCardClickExecuted);
            NewGameCommand = new RelayCommand(param => StartNewGame());

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (sender, e) => _gameModel.UpdateTimer();

            StartNewGame();
            

        }

        private void OnGameUpdated()
        {
            OnPropertyChanged(nameof(Cards));
            OnPropertyChanged(nameof(TimerText));
            OnPropertyChanged(nameof(IsGameOver));

            if (_gameModel.IsGameOver)
            {
                _timer.Stop();
                GameStatus = _gameModel.IsGameWon ? "Felicitări! Ai câștigat!" : "Timpul s-a scurs! Ai pierdut.";
                UpdateUserStatistics(_gameModel.IsGameWon);
            }
        }

        private void OnCardClickExecuted(object parameter)
        {
            if (parameter is CardModel card)
            {
                _gameModel.OnCardClicked(card.Index);
            }
        }

        public void StartNewGame()
        {
            List<string> imagePaths = LoadImages();
            _gameModel.StartNewGame(imagePaths);
            _timer.Start();
            GameStatus = "Jocul a început! Potrivește toate cărțile!";
        }

        private void UpdateUserStatistics(bool gameWon)
        {
            const string usersFile = "users.json";

            if (!File.Exists(usersFile)) return;

            var json = File.ReadAllText(usersFile);
            var userList = System.Text.Json.JsonSerializer.Deserialize<List<UserModel>>(json);

            if (userList == null) return;

            var user = userList.FirstOrDefault(u => u.Username == _currentUser.Username);
            if (user != null)
            {
                user.GamesPlayed++;
                if (gameWon) user.GamesWon++;

                var updatedJson = System.Text.Json.JsonSerializer.Serialize(userList, new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(usersFile, updatedJson);
            }
        }

        private List<string> LoadImages()
        {
            
            List<string> images = new List<string>
            {
                "C:\\Users\\giuli\\Documents\\MVP\\Tema2 - MemoryGame\\Tema2 - MemoryGame\\Fruit\\Fruit1.jpg",
                "C:\\Users\\giuli\\Documents\\MVP\\Tema2 - MemoryGame\\Tema2 - MemoryGame\\Fruit\\Fruit2.jpg",
                "C:\\Users\\giuli\\Documents\\MVP\\Tema2 - MemoryGame\\Tema2 - MemoryGame\\Fruit\\Fruit3.jpg",
                "C:\\Users\\giuli\\Documents\\MVP\\Tema2 - MemoryGame\\Tema2 - MemoryGame\\Fruit\\Fruit4.jpg",
                "C:\\Users\\giuli\\Documents\\MVP\\Tema2 - MemoryGame\\Tema2 - MemoryGame\\Fruit\\Fruit5.jpg",
                "C:\\Users\\giuli\\Documents\\MVP\\Tema2 - MemoryGame\\Tema2 - MemoryGame\\Fruit\\Fruit6.jpg",
                "C:\\Users\\giuli\\Documents\\MVP\\Tema2 - MemoryGame\\Tema2 - MemoryGame\\Fruit\\Fruit7.jpg",
                "C:\\Users\\giuli\\Documents\\MVP\\Tema2 - MemoryGame\\Tema2 - MemoryGame\\Fruit\\Fruit8.jpg"
            };

            return images;
        }


    }
}
