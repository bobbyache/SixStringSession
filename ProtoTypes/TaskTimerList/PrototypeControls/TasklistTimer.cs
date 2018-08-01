using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototypeControls
{
    public partial class TasklistTimer: UserControl
    {
    private Timer timer = new Timer();
    private ListViewItem currentItem = null;
    private int timeLeft = 0;

    public TasklistTimer()
    {
      InitializeComponent();
      InitializeListView();
      InitializeTimer();

      IEnumerable<Task> tasks = GetTasks();
      CreateList(tasks);
    }

    public void Start()
    {
      TurnOnFirstItem();
    }

    private void InitializeTimer()
    {
      timer.Interval = 1000;
      timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      if (timeLeft > 0)
      {
        CountdownCurrentItem();
        // Alternate method
        //int secondsLeft = timeLeft % 60;
        //int minutesLeft = timeLeft / 60;
      }
      else
      {
        TurnOnNextItem();
        //timer.Stop();
        //SystemSounds.Exclamation.Play();
        //MessageBox.Show("Time's up!", "Time has elapsed", MessageBoxButtons.OK);
      }
    }
    private void CountdownCurrentItem()
    {
      timeLeft = timeLeft - 1;
      // Display time remaining as mm:ss
      var timespan = TimeSpan.FromSeconds(timeLeft);
      currentItem.SubItems[1].Text = timespan.ToString(@"mm\:ss");
    }

    private void TurnOnFirstItem()
    {
      if (listView.Items.Count > 0)
      {
        currentItem = listView.Items[0];
        currentItem.ForeColor = Color.Blue;
        timeLeft = int.Parse(currentItem.Tag.ToString());
        timer.Start();
      }
    }

    private void TurnOnNextItem()
    {
      timer.Stop();
      currentItem.ForeColor = Color.Green;

      int index = currentItem.Index;
      if (listView.Items.Count - 1 > index)
      {
        currentItem = listView.Items[index + 1];
        currentItem.ForeColor = Color.Blue;
        timeLeft = int.Parse(currentItem.Tag.ToString());
        timer.Start();
      }
    }

    private void InitializeListView()
    {
      listView.View = View.Details;

      ColumnHeader countdownHeader = new ColumnHeader();
      countdownHeader.Text = "Time";

      ColumnHeader titleHeader = new ColumnHeader();
      titleHeader.Text = "Title";

      listView.Columns.Add(titleHeader);
      listView.Columns.Add(countdownHeader);

      listView.Columns[0].Width = 300;
      listView.Columns[1].Width = 50;
    }

    private void CreateList(IEnumerable<Task> tasks)
    {
      foreach (var task in tasks)
        listView.Items.Add(CreateListViewItem(task));
    }

    private ListViewItem CreateListViewItem(Task task)
    {
      string time = new TimeSpan(0, 0, task.Seconds).ToString(@"mm\:ss");

      ListViewItem item = new ListViewItem();
      item.Tag = task.Seconds;
      item.Text = task.Title;
      item.SubItems.Add(new ListViewItem.ListViewSubItem(item, time));
      item.ForeColor = Color.Red;
      return item;
    }

    private IEnumerable<Task> GetTasks()
    {
      List<Task> tasks = new List<Task>
      {
        new Task("Lead Guitar Techniques - Exercise 1j - Arpeggio pattern using all 6 strings", 5),
        new Task("Lead Guitar Techniques - Exercise 1h - Strumming triplets with rests", 3),
        new Task("Lead Guitar Techniques - Exercise 1t - Heavy palm muting of the top four strings", 10),
        new Task("Lead Guitar Techniques - Exercise 1p - String skipping of the second and thirds", 7)
      };
      return tasks;
    }
  }

  public class Task
  {
    public string Title { get; }
    public int Seconds { get; }

    public Task(string title, int seconds)
    {
      Title = title;
      Seconds = seconds;
    }
  }

}
