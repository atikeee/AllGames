using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CryptoQuipGraphical
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Game g = new Game(new PuzzlePhrase("Deepika Padukone", "Celebrity", new int[] { 1, 1 }));

            string phraseScrambled = g.GetScrambledPuzzlePhrase();
            
            
            Application.Run(new MainForm());
        }
    }
}
