using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoQuipGraphical
{
    public class Game
    {
        // Random number generator
        private static Random randomNumberGenerator = new Random();
        // Stores the puzzle phrase
        private PuzzlePhrase _currentPuzzlePhrase;
        public PuzzlePhrase CurrentPuzzlePhrase
        {
            get
            {
                return this._currentPuzzlePhrase;
            }
        }
        // Stores the letter assignment dictionary 
        private Dictionary<char, char> _letterDictionary;
        public Dictionary<char, char> LetterDictionary
        {
            get
            {
                return this._letterDictionary;
            }
        }
        // Stores the list of hints that have been provided
        private Dictionary<char, char> _hintsDictionary;

        public Game(PuzzlePhrase puzzlePhrase)
        {
            _currentPuzzlePhrase = puzzlePhrase;

            // Assign the letter dictionary
            _letterDictionary = new Dictionary<char, char>();
            assignOneLetterToAnother();

            // Crete the hints dictionary
            _hintsDictionary = new Dictionary<char, char>();
        }

        public string GetScrambledPuzzlePhrase()
        {
            StringBuilder scrambledText = new StringBuilder();
            if (_currentPuzzlePhrase.Phrase != null)
            {
                foreach (char ch1 in _currentPuzzlePhrase.Phrase)
                {
                    if (_letterDictionary.ContainsKey(ch1))
                    {
                        char ch2 = _letterDictionary[ch1];
                        scrambledText.Append(ch2);
                    }
                    else
                    {
                        scrambledText.Append(ch1);
                    }
                }
            }

            return scrambledText.ToString();
        }

        public string GetHint(char ch)
        {
            string hint = "";

            if (_letterDictionary.ContainsKey(ch))
            {

                // Form the string representation of the hint
                hint = string.Format("{0}={1}", ch, _letterDictionary[ch]);
                // Add the hints to the hints dictionary
                this._hintsDictionary[ch] = _letterDictionary[ch];
            }

            return hint;
        }

        private void assignOneLetterToAnother()
        {
            _letterDictionary.Clear();

            char ch1, ch2;
            for (int i = 0; i < 26; i++)
            {
                if (_letterDictionary.Count >= 26)
                {
                    break;
                }

                bool done = false;
                do
                {
                    ch1 = (char)((int)'A' + (int)i);
                    ch2 = (char)((int)'A' + randomNumberGenerator.Next(0, 26));

                    if (_letterDictionary.ContainsKey(ch1))
                    {
                        break;
                    }

                    done = (_letterDictionary.ContainsKey(ch2) == false) && (ch1 != ch2);
                }
                while (!done);

                if (!_letterDictionary.ContainsKey(ch1) && !_letterDictionary.ContainsKey(ch2))
                {
                    _letterDictionary.Add(ch1, ch2);
                    _letterDictionary.Add(ch2, ch1);
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} = {1}", ch1, ch2));
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} = {1}", ch2, ch1));
                }
            }
        }  
    }
}
