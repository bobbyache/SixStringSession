namespace CygSoft.SmartSession.TimerApp
{
    partial class Form1
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
            this.lblCurrentTaskTimeLeft = new System.Windows.Forms.Label();
            this.lblFullSessionTimeLeft = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartPause = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCurrentTaskTimeLeft
            // 
            this.lblCurrentTaskTimeLeft.AutoSize = true;
            this.lblCurrentTaskTimeLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentTaskTimeLeft.Location = new System.Drawing.Point(276, 38);
            this.lblCurrentTaskTimeLeft.Name = "lblCurrentTaskTimeLeft";
            this.lblCurrentTaskTimeLeft.Size = new System.Drawing.Size(245, 63);
            this.lblCurrentTaskTimeLeft.TabIndex = 0;
            this.lblCurrentTaskTimeLeft.Text = "00:00:00";
            // 
            // lblFullSessionTimeLeft
            // 
            this.lblFullSessionTimeLeft.AutoSize = true;
            this.lblFullSessionTimeLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullSessionTimeLeft.Location = new System.Drawing.Point(276, 162);
            this.lblFullSessionTimeLeft.Name = "lblFullSessionTimeLeft";
            this.lblFullSessionTimeLeft.Size = new System.Drawing.Size(245, 63);
            this.lblFullSessionTimeLeft.TabIndex = 1;
            this.lblFullSessionTimeLeft.Text = "00:00:00";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(776, 27);
            this.label1.TabIndex = 2;
            this.label1.Text = "Change chords from A to D and then from  A to C";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStartPause
            // 
            this.btnStartPause.Location = new System.Drawing.Point(259, 254);
            this.btnStartPause.Name = "btnStartPause";
            this.btnStartPause.Size = new System.Drawing.Size(129, 23);
            this.btnStartPause.TabIndex = 3;
            this.btnStartPause.Text = "Start / Pause";
            this.btnStartPause.UseVisualStyleBackColor = true;
            this.btnStartPause.Click += new System.EventHandler(this.btnStartPause_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(394, 254);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnStartPause);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFullSessionTimeLeft);
            this.Controls.Add(this.lblCurrentTaskTimeLeft);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrentTaskTimeLeft;
        private System.Windows.Forms.Label lblFullSessionTimeLeft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartPause;
        private System.Windows.Forms.Button btnReset;
    }
}

