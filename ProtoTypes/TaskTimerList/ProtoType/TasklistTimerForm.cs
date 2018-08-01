using System;
using System.Windows.Forms;

namespace ProtoType
{
  public partial class TasklistTimerForm : Form
  {
    public TasklistTimerForm()
    {
      InitializeComponent();
      btnStart.Click += BtnStart_Click;
    }

    private void BtnStart_Click(object sender, EventArgs e)
    {
      tasklistTimer1.Start();
    }
  }
}
