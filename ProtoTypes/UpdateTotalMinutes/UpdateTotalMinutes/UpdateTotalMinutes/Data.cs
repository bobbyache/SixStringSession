using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UpdateTotalMinutes
{
    public class Data : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private BindingList<TimeItem> list;

        public BindingList<TimeItem> List
        {
            get { return list; }
            set
            {
                list = value;
                OnPropertyChanged("List");
            }
        }

        private TimeItem selectedItem;
        public TimeItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public decimal Total
        {
            get { return list.Sum(x => x.Minutes); }
        }

        private RelayCommand incrementCommand;
        public ICommand IncrementCommand
        {
            get
            {
                if (incrementCommand == null)
                {
                    incrementCommand = new RelayCommand(param => SelectedItem.Minutes += 5, param => true);
                }
                return incrementCommand;
            }
        }

        private RelayCommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(param => list.Add(new TimeItem { Title = Guid.NewGuid().ToString(), Minutes = 5 }),
                        param => true);
                }
                return addCommand;
            }
        }

        private RelayCommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(param => this.Delete(),
                        param => true);
                }
                return deleteCommand;
            }
        }

        private void Delete()
        {
            if (SelectedItem != null)
                list.Remove(SelectedItem);
        }

        public Data()
        {
            this.list = new BindingList<TimeItem>();
            this.list.ListChanged += list_ListChanged;
        }

        public int Count
        {
            get { return list.Count; }
        }

        void list_ListChanged(object sender, ListChangedEventArgs e)
        {
            OnPropertyChanged("Total");
            OnPropertyChanged("Count");
        }

        private void OnPropertyChanged(String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TimeItem : INotifyPropertyChanged
    {
        private string title;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private int minutes;

        public int Minutes
        {
            get { return minutes; }
            set
            {
                minutes = value;
                OnPropertyChanged("Minutes");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
