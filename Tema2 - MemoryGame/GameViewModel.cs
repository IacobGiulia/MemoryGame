using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Tema2___MemoryGame
{
    public class GameViewModel: MainViewModel
    {
        private GameModel _gameModel;
        private DispatcherTimer _timer;

        public string TimerText => $"Time Left: {_gameModel.TimeLeft}s";
        public List<CardModel> Cards => _gameModel.Cards;
        public bool IsGameOver => _gameModel.IsGameOver;

        public GameViewModel()
        {
            _gameModel = new GameModel();
            _gameModel.GameUpdated += OnGameUpdated;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (sender, e) => _gameModel.UpdateTimer();
            _timer.Start();
        }

        private void OnGameUpdated()
        {
            OnPropertyChanged(nameof(Cards));
            OnPropertyChanged(nameof(TimerText));
            OnPropertyChanged(nameof(IsGameOver));
        }

        public void StartNewGame()
        {
            List<string> imagePaths = LoadImages();  // Aici vei încărca căile către imagini
            _gameModel.StartNewGame(imagePaths);
            _timer.Start();
        }


        private List<string> LoadImages()
        {
            // Încarcă căile fișierelor de imagini, presupunând că ai un folder "Images" în directorul de resurse.
            string imageDirectory = @"C:\Users\giuli\Documents\MVP\Tema2 - MemoryGame\Tema2 - MemoryGame\Fruit"; // înlocuiește cu calea corectă către folderul tău cu imagini
            var imagePaths = Directory.GetFiles(imageDirectory, "*.jpg").ToList(); // Poți să folosești și *.jpg, *.jpeg sau alt tip de imagini.

            // Afișează în consolă căile imaginilor pentru debugging
            foreach (var path in imagePaths)
            {
                Console.WriteLine($"Image Path: {path}");
            }

            return imagePaths;
        }
        public void OnCardClicked(int index)
        {
            _gameModel.OnCardClicked(index);
        }

        
    }
}
