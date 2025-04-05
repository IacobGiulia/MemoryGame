using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2___MemoryGame
{
    public class GameModel
    {
        public List<CardModel> Cards { get; set; }
        public int TimeLeft { get; set; }
        public bool IsGameOver { get; set; }
        public int firstCardIndex = -1;
        public int secondCardIndex = -1;

        public event Action GameUpdated;

        public GameModel()
        {
            Cards = new List<CardModel>();
            TimeLeft = 60;  // Jocul începe cu 60 de secunde
            IsGameOver = false;
        }

        public void StartNewGame(List<string> imagePaths)
        {
            var shuffledPaths = imagePaths.Concat(imagePaths).OrderBy(x => Guid.NewGuid()).ToList();  // Amestecă imaginile
            Cards.Clear();

            foreach (var path in shuffledPaths)
            {
                Cards.Add(new CardModel(path));
            }

            TimeLeft = 60;  // Resetăm cronometrul
            IsGameOver = false;
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
            else if (secondCardIndex == -1)
            {
                secondCardIndex = index;
                CheckForMatch();
            }
        }

        private void CheckForMatch()
        {
            if (Cards[firstCardIndex].ImagePath == Cards[secondCardIndex].ImagePath)
            {
                Cards[firstCardIndex].IsMatched = true;
                Cards[secondCardIndex].IsMatched = true;
            }
            else
            {
                Task.Delay(500).ContinueWith(t =>
                {
                    Cards[firstCardIndex].IsFaceUp = false;
                    Cards[secondCardIndex].IsFaceUp = false;
                    NotifyGameUpdated();
                });
            }

            firstCardIndex = -1;
            secondCardIndex = -1;

            if (Cards.All(card => card.IsMatched))
            {
                IsGameOver = true;
                NotifyGameUpdated();
            }
        }

        public void UpdateTimer()
        {
            if (IsGameOver) return;

            TimeLeft--;
            if (TimeLeft <= 0)
            {
                IsGameOver = true;
            }
            NotifyGameUpdated();
        }

        private void NotifyGameUpdated()
        {
            GameUpdated?.Invoke();
        }
    }
}
