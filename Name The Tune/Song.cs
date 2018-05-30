using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NameTheTune
{
    public class Song
    {
        // Name of the song
        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        // Filename of the song
        private string _fileName;
        public string FileName
        {
            get
            {
                return this._fileName;
            }
        }

        // List of hint clips
        private List<Clip> _hintClipList;
        public List<Clip> HintClipList
        {
            get
            {
                return this._hintClipList;
            }
        }

        // Solution Clip
        private Clip _solutionClip;
        public Clip SolutionClip
        {
            get
            {
                return this._solutionClip;
            }
        }


        public Song(string fileName, string name, string clipList, string solutionClip)
        {
            // FileName
            this._fileName = fileName;
            // Name
            this._name = name;
            // List of hint clips
            this._hintClipList = new List<Clip>();
            string[] clipParts = clipList.Split(',');
            foreach (string s in clipParts)
            {
                _hintClipList.Add(new Clip(this, s.Trim()));
            }
            // Solution Clip
            this._solutionClip = new Clip(this, solutionClip);
        }



        public override string ToString()
        {
            StringBuilder s = new StringBuilder();

            s.Append("=======================================");
            s.Append(Environment.NewLine);
            s.Append("Song: ");
            s.Append(this.Name);
            s.Append(Environment.NewLine);
            s.Append("Hint Clips:");
            s.Append(Environment.NewLine);
            int i = 0;
            foreach (Clip c in this.HintClipList)
            {
                s.Append(string.Format("Clip #{0}: {1}", ++i, c));
                s.Append(Environment.NewLine);
            }
            s.Append(string.Format("Solution Clip: {0}", this.SolutionClip));

            return s.ToString();
        }

        public class Clip
        {
            // Song
            private Song _song;
            public Song Song
            {
                get
                {
                    return this._song;
                }
            }

            // Start Time
            private TimeSpan _startTime;
            public TimeSpan StartTime
            {
                get
                {
                    return this._startTime;
                }
            }

            // Duration
            private float _duration;
            public float Duration
            {
                get
                {
                    return this._duration;
                }
            }

            public Clip(Song song, string clip)
            {
                // Assign the song
                this._song = song;

                // Duration
                string[] parts1 = clip.Split('-');
                this._duration = float.Parse(parts1[1].Trim());

                // Start Time
                string[] parts2 = parts1[0].Split(':');
                this._startTime = new TimeSpan(0, int.Parse(parts2[0].Trim()), int.Parse(parts2[1].Trim()));

            }

            public override string ToString()
            {
                return (string.Format("StartTime: {0}, Duration: {1}", _startTime, _duration));
            }
        }
    }
}
