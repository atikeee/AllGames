using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace NameTheTune
{
    public partial class MainForm : Form
    {       
        private static Game theGame;
        private System.Threading.Timer stopTimer = null;
        private static Song.Clip currentClip;
        private static DateTime currentClipPlayStartTime;
        private static TimeSpan amountOfClipPlayed;
        private static bool playingSolutionClip;
        private buttonPlayState btnCurrentPlayButtonState;
        private buttonAnswerState btnAnswerState;

        private enum buttonPlayState
        {
           STATE_NONE,
           STATE_PLAYING,
           STATE_PAUSED
        }

        private enum buttonAnswerState
        {
            STATE_NONE,
            STATE_PLAYING_ANSWER_ONLY,
            STATE_PLAYING_FULL_SONG
        }

        
        public MainForm()
        {
            InitializeComponent();          
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load the game
            theGame = new Game();
            // Update the controls
            updateControls();

            this.axWindowsMediaPlayer.uiMode = "none";
            this.axWindowsMediaPlayer.settings.volume = 100;
        }

        private void btnPrevSong_Click(object sender, EventArgs e)
        {
            // Stop any song that might be playing currently
            MediaPlayerStop();
            if (stopTimer != null)
            {
                // Disable the timer
                stopTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            // Get the previous song
            theGame.GetPrevSingleGame();

            // Update the controls
            this.axWindowsMediaPlayer.uiMode = "none";
            this.btnCurrentPlayButtonState = buttonPlayState.STATE_NONE;
            this.btnAnswerState = buttonAnswerState.STATE_NONE;
            playingSolutionClip = false;
            currentClip = null;
            updateControls();
            this.tbSongInfo.Text = string.Format("Loaded Song {0}/{1}", theGame.SongIndex + 1, theGame.SongFile.listOfSongs.Count);
        }

        private void btnNextSong_Click(object sender, EventArgs e)
        {
            // Stop any song that might be playing currently
            MediaPlayerStop();
            if (stopTimer != null)
            {
                // Disable the timer
                stopTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            // Get the next song
            theGame.GetNextSingleGame();
            
            // Update the controls
            this.axWindowsMediaPlayer.uiMode = "none";
            this.btnCurrentPlayButtonState = buttonPlayState.STATE_NONE;
            this.btnAnswerState = buttonAnswerState.STATE_NONE;
            playingSolutionClip = false;
            currentClip = null;
            updateControls();
            this.tbSongInfo.Text = string.Format("Loaded Song {0}/{1}", theGame.SongIndex + 1, theGame.SongFile.listOfSongs.Count);
        }
       
        private void btnPlayAllClips_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => { btnPlayAllClips_Click(sender, e); }));
                return;
            }

            switch (btnCurrentPlayButtonState)
            {
                case buttonPlayState.STATE_NONE:
                    // Get the next clip
                    currentClip = theGame.CurrentGame.GetNextClip();
                    if (currentClip != null)
                    {
                        amountOfClipPlayed = TimeSpan.FromSeconds(0.0f);

                        // Update the controls
                        updateControls();
                        this.tbSongInfo.Text = string.Format("Playing Song #{0}, Clip {1}/{2} [{3}s]", theGame.SongIndex + 1, theGame.CurrentGame.ClipIndex + 1, theGame.CurrentGame.Song.HintClipList.Count, currentClip.Duration);

                        // Play the next clip
                        if (currentClip != null)
                        {
                            playClip(currentClip);
                        }
                    }
                    break;
                case buttonPlayState.STATE_PAUSED:
                    // Resume play
                    playClip(currentClip);
                    break;
                case buttonPlayState.STATE_PLAYING:
                    // Stop the timer
                    stopTimer.Change(Timeout.Infinite, Timeout.Infinite);
                    // Update the state
                    this.btnCurrentPlayButtonState = buttonPlayState.STATE_PAUSED;
                    // Update the amount of clip played
                    DateTime d1 = currentClipPlayStartTime;
                    DateTime d2 = DateTime.Now;
                    amountOfClipPlayed += (d2 - d1);
                    // Pause play
                    MediaPlayerPause();
                    // Update the controls
                    updateControls();
                    break;
            }
        }

       
        private void btnSolve_Click(object sender, EventArgs e)
        {
            playingSolutionClip = true;
            if (btnAnswerState == buttonAnswerState.STATE_NONE)
            {
                btnAnswerState = buttonAnswerState.STATE_PLAYING_ANSWER_ONLY;
                Song.Clip solutionClip = theGame.CurrentGame.Song.SolutionClip;
                playClip(solutionClip);
            }
            else if (btnAnswerState == buttonAnswerState.STATE_PLAYING_ANSWER_ONLY)
            {
                btnAnswerState = buttonAnswerState.STATE_PLAYING_FULL_SONG;
                playFullSong();
            }
            else if (btnAnswerState == buttonAnswerState.STATE_PLAYING_FULL_SONG)
            {
                btnAnswerState = buttonAnswerState.STATE_PLAYING_ANSWER_ONLY;
                Song.Clip solutionClip = theGame.CurrentGame.Song.SolutionClip;
                playClip(solutionClip);
            }
            

            updateControls();
            this.tbSongInfo.Text = string.Format("Song: {0}...", theGame.CurrentGame.Song.Name);
        }

        private void updateControls()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => { updateControls(); }));
                return;
            }

            this.btnPrevSong.Enabled = !theGame.IsFirstSong();
            this.btnNextSong.Enabled = !theGame.IsLastSong();           
            this.btnPlayAllClips.Enabled = (theGame.CurrentGame != null) && (this.btnAnswerState == buttonAnswerState.STATE_NONE);
            this.btnAnswer.Enabled = (theGame.CurrentGame != null);            
            this.lblSongName.Text = (theGame.CurrentGame != null) ? reverseString(theGame.CurrentGame.Song.Name) : "";

            switch (this.btnCurrentPlayButtonState)
            {
                case buttonPlayState.STATE_NONE:
                    this.btnPlayAllClips.Text = "Play Clips";                    
                    break;
                case buttonPlayState.STATE_PAUSED:
                    this.btnPlayAllClips.Text = "Resume";
                    break;
                case buttonPlayState.STATE_PLAYING:
                    this.btnPlayAllClips.Text = "Pause";
                    break;
            }            

            switch (this.btnAnswerState)
            {
                case buttonAnswerState.STATE_NONE:
                    this.btnAnswer.Text = "Play Answer";
                    break;
                case buttonAnswerState.STATE_PLAYING_ANSWER_ONLY:
                    this.btnAnswer.Text = "Play Full Song";
                    break;
                case buttonAnswerState.STATE_PLAYING_FULL_SONG:
                    this.btnAnswer.Text = "Play Answer";
                    break;
            }
        }

        private void updateControlsPostClipPlay()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => { updateControlsPostClipPlay(); }));
                return;
            }

            this.tbSongInfo.Text = string.Format("Finished Playing Clips for Song #{0}", theGame.SongIndex + 1);
            this.btnPlayAllClips.Text = "Replay Clips";
        }

        private void playClip(Song.Clip clip)
        {  
            TimeSpan tFull = TimeSpan.FromMilliseconds((clip.Duration * 1000));
            TimeSpan tPlayedAlready = amountOfClipPlayed;
            TimeSpan timeLeft = tFull - tPlayedAlready;
            if (stopTimer == null)
            {
                // Create the timer
                stopTimer = new System.Threading.Timer(TimerElapsed, null, (long) timeLeft.TotalMilliseconds, Timeout.Infinite);  
            }
            else
            {
                // Change the timer
                stopTimer.Change((long)(timeLeft.TotalMilliseconds), Timeout.Infinite);
            }
            currentClipPlayStartTime = DateTime.Now;
            
            // Determine the position to start playing from
            double currentPosition = clip.StartTime.TotalSeconds + amountOfClipPlayed.TotalMilliseconds / 1000;
            // Play the clip
            this.axWindowsMediaPlayer.uiMode = "none";
            this.axWindowsMediaPlayer.URL = Path.Combine(theGame.SongFile.songPath, clip.Song.FileName);
            this.axWindowsMediaPlayer.Ctlcontrols.currentPosition = currentPosition;
            MediaPlayerPlay();

            // Set the current state to playing
            this.btnCurrentPlayButtonState = buttonPlayState.STATE_PLAYING;
            updateControls();
        }

        private void playFullSong()
        {
            // Disable the timer
            MediaPlayerStop();
            stopTimer.Change(Timeout.Infinite, Timeout.Infinite);

            // Play the clip
            this.axWindowsMediaPlayer.uiMode = "full";
            this.axWindowsMediaPlayer.URL = Path.Combine(theGame.SongFile.songPath, theGame.CurrentGame.Song.FileName);
            this.axWindowsMediaPlayer.Ctlcontrols.currentPosition = 0.0f;
            MediaPlayerPlay();
        }

        private void TimerElapsed(object obj)
        {
            MediaPlayerStop();
            stopTimer.Change(Timeout.Infinite, Timeout.Infinite);
            
            btnCurrentPlayButtonState = buttonPlayState.STATE_NONE;
            amountOfClipPlayed = TimeSpan.FromMilliseconds(0.0f);
            updateControls();
            
            if (!playingSolutionClip)
            {
                if (!theGame.CurrentGame.IsLastClip())
                {
                    if (theGame.SongFile.pauseLength > 0)
                    {
                        Thread.Sleep(theGame.SongFile.pauseLength);                        
                    }

                    this.btnPlayAllClips_Click(this, null);
                }
                else
                {
                    theGame.CurrentGame.ResetClipIndex();
                    updateControlsPostClipPlay();
                }
            }
        }

        private void MediaPlayerPlay()
        {
            this.axWindowsMediaPlayer.Ctlcontrols.play();
            System.Diagnostics.Debug.WriteLine(string.Format("Playing Clip at {0}...", this.axWindowsMediaPlayer.Ctlcontrols.currentPosition));
        }

        private void MediaPlayerPause()
        {
            this.axWindowsMediaPlayer.Ctlcontrols.pause();
            System.Diagnostics.Debug.WriteLine("Pausing Clip...");
        }

        private void MediaPlayerStop()
        {
            this.axWindowsMediaPlayer.Ctlcontrols.stop();
            System.Diagnostics.Debug.WriteLine("Stopping Clip...");
        }

        private string reverseString(string s)
        {
            string s2 = "";

            for (int i = 0; i < s.Length; i++)
            {
                s2 += s[(s.Length - 1) - i];
            }

            return s2.ToUpper();
        }
    }
}
