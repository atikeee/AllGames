namespace httpserver_testconsole
{
    partial class selectoptions
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
            this.btn_dlg_kill = new System.Windows.Forms.Button();
            this.btn_dlg_reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_dlg_kill
            // 
            this.btn_dlg_kill.Location = new System.Drawing.Point(12, 12);
            this.btn_dlg_kill.Name = "btn_dlg_kill";
            this.btn_dlg_kill.Size = new System.Drawing.Size(75, 23);
            this.btn_dlg_kill.TabIndex = 3;
            this.btn_dlg_kill.Text = "KILL";
            this.btn_dlg_kill.UseVisualStyleBackColor = true;
            // 
            // btn_dlg_reset
            // 
            this.btn_dlg_reset.Location = new System.Drawing.Point(93, 12);
            this.btn_dlg_reset.Name = "btn_dlg_reset";
            this.btn_dlg_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_dlg_reset.TabIndex = 4;
            this.btn_dlg_reset.Text = "RESET";
            this.btn_dlg_reset.UseVisualStyleBackColor = true;
            // 
            // selectoptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 44);
            this.Controls.Add(this.btn_dlg_reset);
            this.Controls.Add(this.btn_dlg_kill);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "selectoptions";
            this.Text = "selectoptions";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_dlg_kill;
        private System.Windows.Forms.Button btn_dlg_reset;
    }
}