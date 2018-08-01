namespace ProtoType
{
  partial class TasklistTimerForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TasklistTimerForm));
      this.tasklistTimer1 = new PrototypeControls.TasklistTimer();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnStart = new System.Windows.Forms.ToolStripButton();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tasklistTimer1
      // 
      this.tasklistTimer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tasklistTimer1.Location = new System.Drawing.Point(0, 25);
      this.tasklistTimer1.Name = "tasklistTimer1";
      this.tasklistTimer1.Size = new System.Drawing.Size(800, 403);
      this.tasklistTimer1.TabIndex = 6;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStart});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(800, 25);
      this.toolStrip1.TabIndex = 4;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // btnStart
      // 
      this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
      this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(51, 22);
      this.btnStart.Text = "Start";
      // 
      // statusStrip1
      // 
      this.statusStrip1.Location = new System.Drawing.Point(0, 428);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(800, 22);
      this.statusStrip1.TabIndex = 5;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // TasklistTimerForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.tasklistTimer1);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.statusStrip1);
      this.Name = "TasklistTimerForm";
      this.Text = "Form1";
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private PrototypeControls.TasklistTimer tasklistTimer1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnStart;
    private System.Windows.Forms.StatusStrip statusStrip1;
  }
}

