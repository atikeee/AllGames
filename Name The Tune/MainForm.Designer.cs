namespace NameTheTune
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnPrevSong = new System.Windows.Forms.Button();
            this.btnPlayAllClips = new System.Windows.Forms.Button();
            this.btnNextSong = new System.Windows.Forms.Button();
            this.btnAnswer = new System.Windows.Forms.Button();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.pnlSongWindow = new System.Windows.Forms.Panel();
            this.tbSongInfo = new System.Windows.Forms.TextBox();
            this.lblSongName = new System.Windows.Forms.Label();
            this.axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.pnlMenu.SuspendLayout();
            this.pnlSongWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrevSong
            // 
            this.btnPrevSong.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevSong.Location = new System.Drawing.Point(217, 3);
            this.btnPrevSong.Name = "btnPrevSong";
            this.btnPrevSong.Size = new System.Drawing.Size(128, 55);
            this.btnPrevSong.TabIndex = 4;
            this.btnPrevSong.Text = "Prev Song";
            this.btnPrevSong.UseVisualStyleBackColor = true;
            this.btnPrevSong.Click += new System.EventHandler(this.btnPrevSong_Click);
            // 
            // btnPlayAllClips
            // 
            this.btnPlayAllClips.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlayAllClips.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayAllClips.Location = new System.Drawing.Point(791, 3);
            this.btnPlayAllClips.Name = "btnPlayAllClips";
            this.btnPlayAllClips.Size = new System.Drawing.Size(113, 55);
            this.btnPlayAllClips.TabIndex = 2;
            this.btnPlayAllClips.Text = "Replay Clips";
            this.btnPlayAllClips.UseVisualStyleBackColor = true;
            this.btnPlayAllClips.Click += new System.EventHandler(this.btnPlayAllClips_Click);
            // 
            // btnNextSong
            // 
            this.btnNextSong.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextSong.Location = new System.Drawing.Point(351, 3);
            this.btnNextSong.Name = "btnNextSong";
            this.btnNextSong.Size = new System.Drawing.Size(128, 55);
            this.btnNextSong.TabIndex = 1;
            this.btnNextSong.Text = "Next Song";
            this.btnNextSong.UseVisualStyleBackColor = true;
            this.btnNextSong.Click += new System.EventHandler(this.btnNextSong_Click);
            // 
            // btnAnswer
            // 
            this.btnAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnswer.Location = new System.Drawing.Point(11, 3);
            this.btnAnswer.Name = "btnAnswer";
            this.btnAnswer.Size = new System.Drawing.Size(189, 55);
            this.btnAnswer.TabIndex = 0;
            this.btnAnswer.Text = "Answer";
            this.btnAnswer.UseVisualStyleBackColor = true;
            this.btnAnswer.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // pnlMenu
            // 
            this.pnlMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMenu.BackColor = System.Drawing.Color.MintCream;
            this.pnlMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMenu.Controls.Add(this.btnPlayAllClips);
            this.pnlMenu.Controls.Add(this.btnPrevSong);
            this.pnlMenu.Controls.Add(this.btnAnswer);
            this.pnlMenu.Controls.Add(this.btnNextSong);
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(916, 65);
            this.pnlMenu.TabIndex = 2;
            // 
            // pnlSongWindow
            // 
            this.pnlSongWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSongWindow.BackColor = System.Drawing.Color.LightBlue;
            this.pnlSongWindow.Controls.Add(this.tbSongInfo);
            this.pnlSongWindow.Controls.Add(this.lblSongName);
            this.pnlSongWindow.Controls.Add(this.axWindowsMediaPlayer);
            this.pnlSongWindow.Location = new System.Drawing.Point(0, 65);
            this.pnlSongWindow.Name = "pnlSongWindow";
            this.pnlSongWindow.Size = new System.Drawing.Size(916, 490);
            this.pnlSongWindow.TabIndex = 3;
            // 
            // tbSongInfo
            // 
            this.tbSongInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSongInfo.BackColor = System.Drawing.Color.Ivory;
            this.tbSongInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSongInfo.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSongInfo.ForeColor = System.Drawing.Color.DarkGreen;
            this.tbSongInfo.Location = new System.Drawing.Point(12, 404);
            this.tbSongInfo.Multiline = true;
            this.tbSongInfo.Name = "tbSongInfo";
            this.tbSongInfo.Size = new System.Drawing.Size(892, 60);
            this.tbSongInfo.TabIndex = 3;
            this.tbSongInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSongName
            // 
            this.lblSongName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSongName.AutoSize = true;
            this.lblSongName.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSongName.ForeColor = System.Drawing.Color.Silver;
            this.lblSongName.Location = new System.Drawing.Point(11, 473);
            this.lblSongName.Name = "lblSongName";
            this.lblSongName.Size = new System.Drawing.Size(78, 9);
            this.lblSongName.TabIndex = 2;
            this.lblSongName.Text = "Woh Ladki Hai Kahan";
            this.lblSongName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // axWindowsMediaPlayer
            // 
            this.axWindowsMediaPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axWindowsMediaPlayer.Enabled = true;
            this.axWindowsMediaPlayer.Location = new System.Drawing.Point(13, 6);
            this.axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
            this.axWindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer.OcxState")));
            this.axWindowsMediaPlayer.Size = new System.Drawing.Size(891, 392);
            this.axWindowsMediaPlayer.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(916, 555);
            this.Controls.Add(this.pnlSongWindow);
            this.Controls.Add(this.pnlMenu);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Name The Tune";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlMenu.ResumeLayout(false);
            this.pnlSongWindow.ResumeLayout(false);
            this.pnlSongWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPrevSong;
        private System.Windows.Forms.Button btnPlayAllClips;
        private System.Windows.Forms.Button btnNextSong;
        private System.Windows.Forms.Button btnAnswer;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel pnlSongWindow;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
        private System.Windows.Forms.Label lblSongName;
        private System.Windows.Forms.TextBox tbSongInfo;
    }
}

