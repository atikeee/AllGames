using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CryptoQuipGraphical
{
    public partial class MainForm : Form
    {
        public static int LETTER_TEXTBOX_WIDTH = 100;
        public static int LETTER_TEXTBOX_HEIGHT = 120;
        public static int LETTER_FONT_SIZE = 72;
        public static int MINIMUM_VERTICAL_OFFSET = 200;
        public const int GAP_BETWEEN_CHARS = 0;
        public const int GAP_BETWEEN_LINES = 5;
        public static Color UNSOLVED_BACKCOLOR = Color.FromArgb(255, 255, 192, 192);
        public static Color UNSOLVED_FORECOLOR = Color.FromArgb(255, 192, 0, 0);
        public static Color SOLVED_BACKCOLOR = Color.FromArgb(255, 192, 255, 192);
        public static Color SOLVED_FORECOLOR = Color.FromArgb(255, 0, 64, 0);
        public static Font LETTERBOX_FONT = new Font("Lucida Console", LETTER_FONT_SIZE, FontStyle.Regular, GraphicsUnit.Point);
        public static Font CATEGORY_FONT = new Font("Georgia", 24f, FontStyle.Italic, GraphicsUnit.Point);

        private static Random randomNumberGenerator = new Random();
        private static PuzzlePhraseFile puzzlePhraseFile;  
        private Game _currentGame;
        private int currentPuzzleIndex; 

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initialize the current game
            _currentGame = null;
            currentPuzzleIndex = -1;
            // Load the list of phrases
            puzzlePhraseFile = PuzzlePhraseFile.GetInstance();
            // Shuffle the phrases
            int newIndex;
            for (int i = 0; i < PuzzlePhraseFile.listOfPuzzlePhrases.Count; i++)
            {
                newIndex = randomNumberGenerator.Next(0, PuzzlePhraseFile.listOfPuzzlePhrases.Count);
                PuzzlePhrase temp = PuzzlePhraseFile.listOfPuzzlePhrases[i];
                PuzzlePhraseFile.listOfPuzzlePhrases[i] = PuzzlePhraseFile.listOfPuzzlePhrases[newIndex];
                PuzzlePhraseFile.listOfPuzzlePhrases[newIndex] = temp;
            }

            // Initalize the controls
            this.btnNewGame.Enabled = true;
            this.btnPrevGame.Enabled = false;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            // Update the current puzzle index
            currentPuzzleIndex = (currentPuzzleIndex == -1) ? 0 : currentPuzzleIndex + 1;
           
            // Enable/Disable the current/previous buttons
            this.btnNewGame.Enabled = (currentPuzzleIndex < PuzzlePhraseFile.listOfPuzzlePhrases.Count - 1);
            this.btnPrevGame.Enabled = (currentPuzzleIndex > 0);

            // Start the game
            startGame(currentPuzzleIndex);
        }

        private void btnPrevGame_Click(object sender, EventArgs e)
        {
            // Update the current puzzle index
            currentPuzzleIndex = (currentPuzzleIndex == -1) ? 0 : currentPuzzleIndex -1;

            // Enable/Disable the current/previous buttons
            this.btnNewGame.Enabled = (currentPuzzleIndex < PuzzlePhraseFile.listOfPuzzlePhrases.Count - 1);
            this.btnPrevGame.Enabled = (currentPuzzleIndex > 0);
            
            // Start the game
            startGame(currentPuzzleIndex);
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.pnlPuzzle.Controls)
            {
                TextBox tb = control as TextBox;
                if (tb != null && tb.Text.Length == 1 && tb.BackColor == UNSOLVED_BACKCOLOR)
                {
                    char ch1 = tb.Text[0];
                    char ch2 = (_currentGame.LetterDictionary.ContainsKey(ch1)) ? _currentGame.LetterDictionary[ch1] : ch1;                    
                    tb.Text = ch2.ToString();
                    tb.BackColor = SOLVED_BACKCOLOR;
                    tb.ForeColor = SOLVED_FORECOLOR;
                }
            }
        }

        private void startGame(int index)
        {
            // Create the new game
            if (index != -1)
            {
                PuzzlePhrase puzzle = PuzzlePhraseFile.listOfPuzzlePhrases[index];
                _currentGame = new Game(puzzle);
                displayPuzzle(_currentGame.GetScrambledPuzzlePhrase(), _currentGame.CurrentPuzzlePhrase.Category, _currentGame.CurrentPuzzlePhrase.WordsPerLine);

                this.tbHint.Focus();
            }
        }
      
        private void tbLetterBox_DoubleClick(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;

            if (t != null && t.BackColor == UNSOLVED_BACKCOLOR)
            {
                char ch1, ch2;
                ch1 = (sender as TextBox).Text[0];
                if (_currentGame.LetterDictionary.ContainsKey(ch1))
                {
                    ch2 = _currentGame.LetterDictionary[ch1];
                    string hint = _currentGame.GetHint(ch1);
                    updateTextBoxesWithHint(hint);
                }
            }
        }

        private TextBox getCategoryTextBox()
        {
            TextBox tbCategory = new TextBox();
            tbCategory.Name = string.Format("textboxCategory");
            tbCategory.Size = new Size(700, 48);
            tbCategory.Multiline = true;
            tbCategory.Font = CATEGORY_FONT;
            tbCategory.BorderStyle = BorderStyle.FixedSingle;
            tbCategory.TextAlign = HorizontalAlignment.Center;
            tbCategory.ReadOnly = true;

            return tbCategory;
        }

        private TextBox getLetterTextBox(int charNumber)
        {
            TextBox tbLetterBox = new TextBox();
            tbLetterBox.Name = string.Format("textbox{0}", charNumber);
            tbLetterBox.Size = new Size(LETTER_TEXTBOX_WIDTH, LETTER_TEXTBOX_WIDTH);
            tbLetterBox.Multiline = true;
            tbLetterBox.Font = LETTERBOX_FONT;
            tbLetterBox.BorderStyle = BorderStyle.FixedSingle;
            tbLetterBox.TextAlign = HorizontalAlignment.Center;
            tbLetterBox.DoubleClick += new EventHandler(tbLetterBox_DoubleClick);
            tbLetterBox.ReadOnly = true;

            return tbLetterBox;
        }

      
        private void displayPuzzle(string textToDisplay, string category, int[] wordsPerLine)
        {
            // Clear existing controls
            foreach(Control control in this.pnlPuzzle.Controls)
            {
                TextBox tb = control as TextBox;
                tb.DoubleClick -= tbLetterBox_DoubleClick;                
            }
            this.pnlPuzzle.Controls.Clear();
            this.tbHint.Clear();

            

            // Set up the category text box
            TextBox tbCategory = getCategoryTextBox();
            int horizontalOffset = (this.pnlPuzzle.ClientSize.Width / 2 - tbCategory.Size.Width / 2);
            int verticalOffset = 20;
            tbCategory.BackColor = Color.FromArgb(255, 255, 255, 192);
            tbCategory.ForeColor = Color.FromArgb(255, 0, 64, 0);
            tbCategory.Location = new Point(horizontalOffset, verticalOffset);
            tbCategory.Text = category;
            this.pnlPuzzle.Controls.Add(tbCategory);


            List<string> lines = getLines(textToDisplay, wordsPerLine);

            int lineNumber = 1;
            foreach (string line in lines)
            {
                printLine(line, lineNumber, GAP_BETWEEN_CHARS, GAP_BETWEEN_LINES);
                lineNumber++;
            }
        }

        private List<string> getLines(string text, int[] wordsPerLine)
        {
            List<string> lines = new List<string>();

            int wordsInLine;
            string[] words = text.Split(' ');
            int wordIndex = 0;
            string line;
            for (int i = 0; i < wordsPerLine.Length; i++)
            {
                line = "";
                wordsInLine = wordsPerLine[i];
                for (int j = 0; j < wordsInLine; j++)
                {
                    line += words[j + wordIndex];
                    if (j < wordsInLine - 1)
                    {
                        line += " ";
                    }
                }
                lines.Add(line);

                wordIndex += wordsInLine;
            }

            return lines;
        }

        private void printLine(string line, int lineNumber, int gapBetweenChars, int gapBetweenLines)
        {
            int horizontalOffset = (this.pnlPuzzle.ClientSize.Width / 2 - (LETTER_TEXTBOX_WIDTH * line.Length) / 2);
            int verticalOffset = MINIMUM_VERTICAL_OFFSET;
            Point p = new Point();
            char ch;
            for (int i = 0; i < line.Length; i++)
            {
                ch = line[i];                
                if (line[i] != ' ')
                {
                    TextBox t = getLetterTextBox(i);
                    p.X = (i == 0) ? horizontalOffset : (horizontalOffset + (i * (LETTER_TEXTBOX_WIDTH + gapBetweenChars)));
                    p.Y = (lineNumber == 1) ? verticalOffset : (verticalOffset + (lineNumber - 1) * (LETTER_TEXTBOX_HEIGHT + gapBetweenLines));
                    t.Location = p;
                    t.Text = ch.ToString();
                    if (!_currentGame.LetterDictionary.ContainsKey(ch))
                    {
                        t.BackColor = SOLVED_BACKCOLOR;
                        t.ForeColor = SOLVED_FORECOLOR;                        
                    }
                    else
                    {
                        t.BackColor = UNSOLVED_BACKCOLOR;
                        t.ForeColor = UNSOLVED_FORECOLOR;                        
                    }

                    this.pnlPuzzle.Controls.Add(t);
                }               
            }
        }

       
        private void updateTextBoxesWithHint(string hint)
        {
            this.tbHint.AppendText(hint);
            this.tbHint.AppendText(Environment.NewLine);

            string [] letters = hint.Split('=');
            char ch1 = letters[0][0];
            char ch2 = letters[1][0];

            foreach(Control control in this.pnlPuzzle.Controls)
            {
                TextBox t = control as TextBox;
                if (t.Text.Length == 1 && t.BackColor == UNSOLVED_BACKCOLOR)
                {
                    char ch = t.Text[0];
                    if (ch == ch1)
                    {
                        t.BackColor = SOLVED_BACKCOLOR;
                        t.ForeColor = SOLVED_FORECOLOR;
                        t.Text = ch2.ToString();                        
                    }
                }
            }
        }
  
        private void btnIncreaseSize_Click(object sender, EventArgs e)
        {
            LETTER_FONT_SIZE += (int) (LETTER_FONT_SIZE * 0.1);
            LETTER_TEXTBOX_HEIGHT += (int) (LETTER_TEXTBOX_HEIGHT * 0.1);
            LETTER_TEXTBOX_WIDTH += (int) (LETTER_TEXTBOX_WIDTH * 0.1);
            LETTERBOX_FONT = new Font("Lucida Console", LETTER_FONT_SIZE, FontStyle.Regular, GraphicsUnit.Point);

            startGame(currentPuzzleIndex);

        }

        private void btnDecreaseSize_Click(object sender, EventArgs e)
        {
            LETTER_FONT_SIZE -= (int) (LETTER_FONT_SIZE * 0.1);
            LETTER_TEXTBOX_HEIGHT -= (int) (LETTER_TEXTBOX_HEIGHT * 0.1);
            LETTER_TEXTBOX_WIDTH -= (int) (LETTER_TEXTBOX_WIDTH * 0.1);
            LETTERBOX_FONT = new Font("Lucida Console", LETTER_FONT_SIZE, FontStyle.Regular, GraphicsUnit.Point);

            startGame(currentPuzzleIndex);
        }             
    }
}
