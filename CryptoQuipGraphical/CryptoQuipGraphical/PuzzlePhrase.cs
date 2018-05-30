using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoQuipGraphical
{
    public class PuzzlePhrase
    {
        // Phrase
        private string _phrase;
        public string Phrase
        {
            get
            {
                return this._phrase.ToUpper();
            }

            set
            {
                this._phrase = value;
            }
        }

        // Category
        private string _category;
        public string Category
        {
            get
            {
                return this._category;
            }

            set
            {
                this._category = value;
            }
        }

        // Words Per Line
        private int[] _wordsPerLine;
        public int[] WordsPerLine
        {
            get
            {
                return this._wordsPerLine;
            }

            set
            {
                this._wordsPerLine = value;
            }
        }

        public PuzzlePhrase(string phrase, string category, int[] wordsPerLine)
        {
            // Phrase
            this._phrase = phrase;
            // Category
            this._category = category;
            // Words Per Line
            this._wordsPerLine = wordsPerLine;
        }
    }
}
