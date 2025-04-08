using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Tema2___MemoryGame
{
    public class GameModel
    {
        public ObservableCollection<CardModel> Cards { get; private set; }
        public int TimeLeft { get; private set; }
        public bool IsGameOver { get; private set; }
        public bool IsGameWon { get; private set; }
        private int firstCardIndex = -1;
        private int secondCardIndex = -1;

        public event Action GameUpdated;

        public GameModel()
        {
            Cards = new ObservableCollection<CardModel>();
            TimeLeft = 60;
            IsGameOver = false;
            IsGameWon = false;
        }

        public void StartNewGame(List<string> imagePaths)
        {
            
            var shuffledPaths = imagePaths.Concat(imagePaths).OrderBy(x => Guid.NewGuid()).ToList();
            Cards.Clear();

            for (int i = 0; i < shuffledPaths.Count; i++)
            {
                Cards.Add(new CardModel(shuffledPaths[i], i));
            }

            TimeLeft = 60;
            IsGameOver = false;
            IsGameWon = false;
            firstCardIndex = -1;
            secondCardIndex = -1;
            NotifyGameUpdated();
        }

        public void OnCardClicked(int index)
        {
            if (Cards[index].IsFaceUp || Cards[index].IsMatched || IsGameOver) return;

            Cards[index].IsFaceUp = true;
            NotifyGameUpdated();

            if (firstCardIndex == -1)
            {
                firstCardIndex = index;
            }
            else if (secondCardIndex == -1 && firstCardIndex != index)
            {
                secondCardIndex = index;
                CheckForMatch();
            }
        }

        private void CheckForMatch()
        {
            if (Cards[firstCardIndex].ActualImagePath == Cards[secondCardIndex].ActualImagePath)
            {
                Cards[firstCardIndex].IsMatched = true;
                Cards[secondCardIndex].IsMatched = true;

                if (Cards.All(card => card.IsMatched))
                {
                    IsGameWon = true;
                    IsGameOver = true;
                    NotifyGameUpdated();
                }
            }
            else
            {
                
                var firstIndex = firstCardIndex;
                var secondIndex = secondCardIndex;

                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    Task.Delay(750).ContinueWith(t =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Cards[firstIndex].IsFaceUp = false;
                            Cards[secondIndex].IsFaceUp = false;
                            NotifyGameUpdated();
                        });
                    });
                }));
            }

            firstCardIndex = -1;
            secondCardIndex = -1;
        }

        public void UpdateTimer()
        {
            if (IsGameOver) return;

            TimeLeft--;
            if (TimeLeft <= 0)
            {
                IsGameOver = true;
                IsGameWon = false;
            }
            NotifyGameUpdated();
        }

        private void NotifyGameUpdated()
        {
            GameUpdated?.Invoke();
        }

    }

}
