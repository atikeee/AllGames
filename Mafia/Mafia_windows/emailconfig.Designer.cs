namespace sufflearray
{
    partial class EmailConfig
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_user = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_smtp = new System.Windows.Forms.TextBox();
            this.textBox_pass = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox_ssl = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "USER";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "PASS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(119, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "PORT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(119, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "SMTP";
            // 
            // textBox_user
            // 
            this.textBox_user.Location = new System.Drawing.Point(203, 77);
            this.textBox_user.Name = "textBox_user";
            this.textBox_user.Size = new System.Drawing.Size(252, 22);
            this.textBox_user.TabIndex = 5;
            this.textBox_user.Text = "sd.gcf.rf";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(203, 186);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(252, 22);
            this.textBox_port.TabIndex = 6;
            this.textBox_port.Text = "587";
            // 
            // textBox_smtp
            // 
            this.textBox_smtp.Location = new System.Drawing.Point(203, 242);
            this.textBox_smtp.Name = "textBox_smtp";
            this.textBox_smtp.Size = new System.Drawing.Size(252, 22);
            this.textBox_smtp.TabIndex = 7;
            this.textBox_smtp.Text = "smtp.gmail.com";
            // 
            // textBox_pass
            // 
            this.textBox_pass.Location = new System.Drawing.Point(203, 130);
            this.textBox_pass.Name = "textBox_pass";
            this.textBox_pass.Size = new System.Drawing.Size(252, 22);
            this.textBox_pass.TabIndex = 9;
            this.textBox_pass.Text = "SanDiego123$";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(265, 358);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 34);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox_ssl
            // 
            this.checkBox_ssl.AutoSize = true;
            this.checkBox_ssl.Checked = true;
            this.checkBox_ssl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ssl.Location = new System.Drawing.Point(203, 300);
            this.checkBox_ssl.Name = "checkBox_ssl";
            this.checkBox_ssl.Size = new System.Drawing.Size(56, 21);
            this.checkBox_ssl.TabIndex = 11;
            this.checkBox_ssl.Text = "SSL";
            this.checkBox_ssl.UseVisualStyleBackColor = true;
            // 
            // EmailConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 432);
            this.Controls.Add(this.checkBox_ssl);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_pass);
            this.Controls.Add(this.textBox_smtp);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_user);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EmailConfig";
            this.Text = "EmailConfig";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_user;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_smtp;
        private System.Windows.Forms.TextBox textBox_pass;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox_ssl;
    }
}