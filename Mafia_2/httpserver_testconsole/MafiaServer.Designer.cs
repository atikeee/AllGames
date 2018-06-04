namespace httpserver_testconsole
{
    partial class MafiaServer
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnday = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_nightend = new System.Windows.Forms.Button();
            this.btn_nightstart = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxReport = new System.Windows.Forms.TextBox();
            this.buttonend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(210, 83);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 21);
            this.comboBox1.TabIndex = 16;
            // 
            // btnday
            // 
            this.btnday.Enabled = false;
            this.btnday.Location = new System.Drawing.Point(314, 72);
            this.btnday.Margin = new System.Windows.Forms.Padding(2);
            this.btnday.Name = "btnday";
            this.btnday.Size = new System.Drawing.Size(65, 30);
            this.btnday.TabIndex = 15;
            this.btnday.Text = "Day Kill";
            this.btnday.UseVisualStyleBackColor = true;
            this.btnday.Click += new System.EventHandler(this.btnday_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_refresh.Location = new System.Drawing.Point(511, 14);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(113, 53);
            this.btn_refresh.TabIndex = 14;
            this.btn_refresh.Text = "REFRESH PLAYER";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // btn_start
            // 
            this.btn_start.Enabled = false;
            this.btn_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_start.Location = new System.Drawing.Point(12, 12);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(84, 55);
            this.btn_start.TabIndex = 13;
            this.btn_start.Text = "START";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_nightend
            // 
            this.btn_nightend.Enabled = false;
            this.btn_nightend.Location = new System.Drawing.Point(314, 14);
            this.btn_nightend.Name = "btn_nightend";
            this.btn_nightend.Size = new System.Drawing.Size(66, 31);
            this.btn_nightend.TabIndex = 12;
            this.btn_nightend.Text = "NightEnd";
            this.btn_nightend.UseVisualStyleBackColor = true;
            this.btn_nightend.Click += new System.EventHandler(this.btn_nightend_Click);
            // 
            // btn_nightstart
            // 
            this.btn_nightstart.Enabled = false;
            this.btn_nightstart.Location = new System.Drawing.Point(225, 14);
            this.btn_nightstart.Name = "btn_nightstart";
            this.btn_nightstart.Size = new System.Drawing.Size(72, 31);
            this.btn_nightstart.TabIndex = 11;
            this.btn_nightstart.Text = "NightStart";
            this.btn_nightstart.UseVisualStyleBackColor = true;
            this.btn_nightstart.Click += new System.EventHandler(this.btn_nightstart_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(385, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 577);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Players";
            // 
            // textBoxReport
            // 
            this.textBoxReport.Location = new System.Drawing.Point(12, 108);
            this.textBoxReport.Multiline = true;
            this.textBoxReport.Name = "textBoxReport";
            this.textBoxReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReport.Size = new System.Drawing.Size(368, 542);
            this.textBoxReport.TabIndex = 9;
            // 
            // buttonend
            // 
            this.buttonend.Enabled = false;
            this.buttonend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonend.Location = new System.Drawing.Point(112, 13);
            this.buttonend.Name = "buttonend";
            this.buttonend.Size = new System.Drawing.Size(84, 55);
            this.buttonend.TabIndex = 17;
            this.buttonend.Text = "END";
            this.buttonend.UseVisualStyleBackColor = true;
            this.buttonend.Click += new System.EventHandler(this.buttonend_Click);
            // 
            // MafiaServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 691);
            this.Controls.Add(this.buttonend);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnday);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_nightend);
            this.Controls.Add(this.btn_nightstart);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxReport);
            this.Name = "MafiaServer";
            this.Text = "MafiaServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MafiaServer_FormClosing);
            this.Load += new System.EventHandler(this.MafiaServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnday;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_nightend;
        private System.Windows.Forms.Button btn_nightstart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxReport;
        private System.Windows.Forms.Button buttonend;
    }
}