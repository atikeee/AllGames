namespace CryptoQuipGraphical
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
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnDecreaseSize = new System.Windows.Forms.Button();
            this.btnIncreaseSize = new System.Windows.Forms.Button();
            this.btnSolve = new System.Windows.Forms.Button();
            this.btnPrevGame = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.pnlPuzzle = new System.Windows.Forms.Panel();
            this.tbHint = new System.Windows.Forms.TextBox();
            this.lblHint = new System.Windows.Forms.Label();
            this.pnlMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMenu.BackColor = System.Drawing.Color.White;
            this.pnlMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMenu.Controls.Add(this.btnDecreaseSize);
            this.pnlMenu.Controls.Add(this.btnIncreaseSize);
            this.pnlMenu.Controls.Add(this.btnSolve);
            this.pnlMenu.Controls.Add(this.btnPrevGame);
            this.pnlMenu.Controls.Add(this.btnNewGame);
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(1102, 76);
            this.pnlMenu.TabIndex = 0;
            // 
            // btnDecreaseSize
            // 
            this.btnDecreaseSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDecreaseSize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDecreaseSize.BackgroundImage")));
            this.btnDecreaseSize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDecreaseSize.Location = new System.Drawing.Point(744, 11);
            this.btnDecreaseSize.Name = "btnDecreaseSize";
            this.btnDecreaseSize.Size = new System.Drawing.Size(60, 55);
            this.btnDecreaseSize.TabIndex = 4;
            this.btnDecreaseSize.UseVisualStyleBackColor = true;
            this.btnDecreaseSize.Click += new System.EventHandler(this.btnDecreaseSize_Click);
            // 
            // btnIncreaseSize
            // 
            this.btnIncreaseSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIncreaseSize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnIncreaseSize.BackgroundImage")));
            this.btnIncreaseSize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIncreaseSize.Location = new System.Drawing.Point(678, 11);
            this.btnIncreaseSize.Name = "btnIncreaseSize";
            this.btnIncreaseSize.Size = new System.Drawing.Size(60, 55);
            this.btnIncreaseSize.TabIndex = 3;
            this.btnIncreaseSize.Text = "Inc";
            this.btnIncreaseSize.UseVisualStyleBackColor = true;
            this.btnIncreaseSize.Click += new System.EventHandler(this.btnIncreaseSize_Click);
            // 
            // btnSolve
            // 
            this.btnSolve.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSolve.BackgroundImage")));
            this.btnSolve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSolve.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolve.Location = new System.Drawing.Point(-1, -1);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(99, 76);
            this.btnSolve.TabIndex = 2;
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // btnPrevGame
            // 
            this.btnPrevGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevGame.Location = new System.Drawing.Point(822, 9);
            this.btnPrevGame.Name = "btnPrevGame";
            this.btnPrevGame.Size = new System.Drawing.Size(130, 57);
            this.btnPrevGame.TabIndex = 1;
            this.btnPrevGame.Text = "Prev Game";
            this.btnPrevGame.UseVisualStyleBackColor = true;
            this.btnPrevGame.Click += new System.EventHandler(this.btnPrevGame_Click);
            // 
            // btnNewGame
            // 
            this.btnNewGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewGame.Location = new System.Drawing.Point(958, 9);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(131, 57);
            this.btnNewGame.TabIndex = 0;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // pnlPuzzle
            // 
            this.pnlPuzzle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPuzzle.Location = new System.Drawing.Point(0, 79);
            this.pnlPuzzle.Name = "pnlPuzzle";
            this.pnlPuzzle.Size = new System.Drawing.Size(953, 608);
            this.pnlPuzzle.TabIndex = 14;
            // 
            // tbHint
            // 
            this.tbHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHint.ForeColor = System.Drawing.Color.Green;
            this.tbHint.Location = new System.Drawing.Point(959, 119);
            this.tbHint.Multiline = true;
            this.tbHint.Name = "tbHint";
            this.tbHint.Size = new System.Drawing.Size(131, 568);
            this.tbHint.TabIndex = 15;
            this.tbHint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblHint
            // 
            this.lblHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHint.ForeColor = System.Drawing.Color.Teal;
            this.lblHint.Location = new System.Drawing.Point(982, 79);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(90, 37);
            this.lblHint.TabIndex = 16;
            this.lblHint.Text = "Hints";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1102, 688);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.tbHint);
            this.Controls.Add(this.pnlPuzzle);
            this.Controls.Add(this.pnlMenu);
            this.Name = "MainForm";
            this.Text = "CryptoQuip";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Panel pnlPuzzle;
        private System.Windows.Forms.TextBox tbHint;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.Button btnPrevGame;
        private System.Windows.Forms.Button btnDecreaseSize;
        private System.Windows.Forms.Button btnIncreaseSize;
    }
}

