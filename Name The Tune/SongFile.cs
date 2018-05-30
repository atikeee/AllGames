using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace NameTheTune
{
    public class SongFile
    {
        // The one and only SongFile instance
        private static SongFile _instance;

        // Constants
        private const string FILENAME = @"songlist.txt";
        private const string SONG_PATH_IDENTIFIER = @"Song Path";
        private const string SONG_IDENTIFIER = @"Songs";
        private const string PAUSE_IDENTIFIER = @"Pause Between Clips (in ms)";
        
        public List<Song> listOfSongs;
        public string songPath;
        public int pauseLength;


        public static SongFile GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SongFile();
            }

            return _instance;
        }

        public void ShuffleSongs()
        {
            if (this.listOfSongs != null)
            {

            }
        }

        private SongFile()
        {
            this.Read(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().FullName), FILENAME));
        }

        private void Read(string fileName)
        {
            bool bReadingSongs = false;

            listOfSongs = new List<Song>();
            string line;
            using (StreamReader f = new StreamReader(fileName))
            {
                while ((line = f.ReadLine()) != null)
                {
                    // Read the line
                    line = line.Trim();

                    if (line.StartsWith(SONG_PATH_IDENTIFIER))
                    {
                        string[] items = line.Split('=');
                        this.songPath = items[1].Trim();

                    }
                    else if (line.StartsWith(PAUSE_IDENTIFIER))
                    {
                        string[] items = line.Split('=');
                        this.pauseLength = int.Parse(items[1].Trim());
                    }
                    else if (line.StartsWith(SONG_IDENTIFIER))
                    {
                        bReadingSongs = true;
                    }
                    else if (bReadingSongs)
                    {
                        // Split the strings by dashes
                        string[] items = line.Split('|');
                        // Read the song
                        Song s = new Song(items[0].Trim(), items[1].Trim(), items[2].Trim(), items[3].Trim());
                        // Add the song to the list
                        listOfSongs.Add(s);
                    }
                }
            }
        }

    }
}
