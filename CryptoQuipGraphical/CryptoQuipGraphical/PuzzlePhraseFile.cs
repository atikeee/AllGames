using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace CryptoQuipGraphical
{
    public class PuzzlePhraseFile
    {
        private const string FILENAME = @"phraselist.txt";

        private static PuzzlePhraseFile _instance;

        public static List<PuzzlePhrase> listOfPuzzlePhrases;

        public static PuzzlePhraseFile GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PuzzlePhraseFile();
            }

            return _instance;
        }

        private PuzzlePhraseFile()
        {
            this.Read(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().FullName), FILENAME));
        }

        private void Read(string fileName)
        {
            listOfPuzzlePhrases = new List<PuzzlePhrase>();
            string line;
            using (StreamReader f = new StreamReader(fileName))
            {
                while ((line = f.ReadLine()) != null)
                {
                    // Read the line
                    line = line.Trim();
                    // Split the strings by comma
                    string[] items = line.Split('|');
                    // Determine the words per line
                    string[] wordsPerLine = items[2].Split('-');
                    int[] wpl = new int[wordsPerLine.Length];
                    int index = 0;
                    foreach(string s in wordsPerLine)
                    {
                        wpl[index++] = int.Parse(s.Trim());
                    }
                    // Create a new puzzle phrase object
                    PuzzlePhrase p = new PuzzlePhrase(items[0].Trim(), items[1].Trim(), wpl);
                    // Add the new object to the list
                    listOfPuzzlePhrases.Add(p);
                }
            }
        }
    }
}
