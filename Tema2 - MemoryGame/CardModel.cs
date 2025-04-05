using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2___MemoryGame
{
    public class CardModel
    {
        public string ImagePath { get; set; }
        public bool IsFaceUp { get; set; }
        public bool IsMatched { get; set; }

        public CardModel(string imagePath)
        {
            ImagePath = imagePath;
            IsFaceUp = false;
            IsMatched = false;
        }

    }
}
