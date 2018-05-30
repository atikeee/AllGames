using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NameTheTune
{
    public class Game
    {
        private SingleGame _currentGame;
        public SingleGame CurrentGame
        {
            get
            {
                return this._currentGame;
            }
        }

        private SongFile _theSongFile;
        public SongFile SongFile
        {
            get
            {
                return this._theSongFile;
            }
        }


        private int _songIndex;
        public int SongIndex
        {
            get
            {
                return _songIndex;
            }
        }

        private Random _randomNumberGenerator;

        public Game()
        {
            // Initialize the random number generator
            _randomNumberGenerator = new Random();
            // Initialize the song file
            _theSongFile = SongFile.GetInstance();
            // Shuffle the songs
            ShuffleSongs();

            _currentGame = null;
            _songIndex = -1;
        }

        public void ShuffleSongs()
        {
            Song temp;
            int newIndex;
            for (int i = 0; i < _theSongFile.listOfSongs.Count; i++)
            {
                newIndex = _randomNumberGenerator.Next(0, _theSongFile.listOfSongs.Count);

                temp = _theSongFile.listOfSongs[i];
                _theSongFile.listOfSongs[i] = _theSongFile.listOfSongs[newIndex];
                _theSongFile.listOfSongs[newIndex] = temp;
            }

            // Print the list of songs
            foreach (Song s in _theSongFile.listOfSongs)
            {
                System.Diagnostics.Debug.WriteLine(s);
            }
        }

        public SingleGame GetPrevSingleGame()
        {
            Song song = GetPrevSong();

            if (song != null)
            {
                _currentGame = new SingleGame(song);
                System.Diagnostics.Debug.WriteLine(string.Format("Game with Song: {0}", song));
            }

            return _currentGame;
        }



        public SingleGame GetNextSingleGame()
        {
            Song song = GetNextSong();

            if (song != null)
            {
                _currentGame = new SingleGame(song);
            }

            return _currentGame;
        }

        public Song GetPrevSong()
        {
            Song s = null;

            if (this._songIndex >= 0)
            {
                this._songIndex--;
            }

            if (this._songIndex >= 0)
            {
                s = this._theSongFile.listOfSongs[this._songIndex];
                System.Diagnostics.Debug.WriteLine("----------Got new song --------------");
                System.Diagnostics.Debug.WriteLine(string.Format("{0}", s));
            }

            return s;
        }



        public Song GetNextSong()
        {
            Song s = null;

            if (this._songIndex == -1)
            {
                this._songIndex = 0;
            }
            else if (this._songIndex < this._theSongFile.listOfSongs.Count)
            {
                this._songIndex++;
            }


            if (this._songIndex >= 0 && this._songIndex < this._theSongFile.listOfSongs.Count)
            {
                s = this._theSongFile.listOfSongs[this._songIndex];
                System.Diagnostics.Debug.WriteLine("----------Got new song --------------");
                System.Diagnostics.Debug.WriteLine(string.Format("{0}", s));
            }

            return s;
        }

        public bool IsFirstSong()
        {
            return (this._songIndex == -1 || this._songIndex == 0);
        }

        public bool IsLastSong()
        {
            return (this._songIndex >= this._theSongFile.listOfSongs.Count - 1);
        }
    }

    public class SingleGame
    {
        private Song _song;
        public Song Song
        {
            get
            {
                return this._song;
            }
        }

        private int _clipIndex;
        public int ClipIndex
        {
            get
            {
                return this._clipIndex;
            }
        }

        public SingleGame(Song song)
        {
            this._song = song;
            this._clipIndex = -1;
        }

        public Song.Clip GetPrevClip()
        {
            Song.Clip clip = null;

            if (this._clipIndex >= 0)
            {
                // Increment the clip index
                this._clipIndex--;
            }

            if (_clipIndex >= 0)
            {
                // Get the next clip
                clip = this._song.HintClipList[this._clipIndex];
                System.Diagnostics.Debug.WriteLine("------------Got New Clip-------------------");
                System.Diagnostics.Debug.WriteLine(string.Format("Clip: {0}", clip));
            }

            return clip;
        }


        public void ResetClipIndex()
        {
            _clipIndex = -1;            
        }

        public Song.Clip GetNextClip()
        {
            Song.Clip clip = null;

            if (this._clipIndex == -1)
            {
                // Set the clip index to 0
                this._clipIndex = 0;
            }
            else if (this._clipIndex < this._song.HintClipList.Count)
            {
                // Increment the clip index
                this._clipIndex++;
            }

            if (_clipIndex >= 0 && _clipIndex < this._song.HintClipList.Count)
            {
                // Get the clip
                clip = this._song.HintClipList[this._clipIndex];
                System.Diagnostics.Debug.WriteLine("------------Got New Clip-------------------");
                System.Diagnostics.Debug.WriteLine(string.Format("Clip: {0}", clip));
            }

            return clip;
        }

        public bool IsFirstClip()
        {
            return (this._clipIndex == 0 || this._clipIndex == -1);
        }

        public bool IsLastClip()
        {
            return (this._clipIndex >= this._song.HintClipList.Count - 1);
        }
    }
}
