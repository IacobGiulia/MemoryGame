using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace Tema2___MemoryGame
{
    public class CardModel : MainViewModel
    {
        private string _imagePath;
        private bool _isFaceUp;
        private bool _isMatched;
        private int _index;

        public string ImagePath
        {
            get => _isFaceUp || _isMatched ? _imagePath : "C:\\Users\\giuli\\Documents\\MVP\\Tema2 - MemoryGame\\Tema2 - MemoryGame\\Card\\cardback.jpg";
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        public string ActualImagePath => _imagePath;

        public bool IsFaceUp
        {
            get => _isFaceUp;
            set
            {
                _isFaceUp = value;
                OnPropertyChanged(nameof(IsFaceUp));
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        public bool IsMatched
        {
            get => _isMatched;
            set
            {
                _isMatched = value;
                OnPropertyChanged(nameof(IsMatched));
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        public CardModel(string imagePath, int index)
        {
            _imagePath = imagePath;
            _index = index;
            _isFaceUp = false;
            _isMatched = false;
        }
    }
}
