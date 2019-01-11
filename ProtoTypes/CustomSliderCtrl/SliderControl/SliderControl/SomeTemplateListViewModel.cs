using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SliderControl
{
    public class SomeTemplateListViewModel : ViewModelBase
    {
        private BindingList<SomeTemplateViewModel> list = new BindingList<SomeTemplateViewModel>();
        public BindingList<SomeTemplateViewModel> List
        {
            get { return list; }
            set
            {
                list = value;
                RaisePropertyChanged();
            }
        }

        private SomeTemplateViewModel selectedItem;
        public SomeTemplateViewModel SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                Set(() => SelectedItem, ref selectedItem, value);
            }
        }

        public RelayCommand IncrementCommand { get; private set; }
        public RelayCommand DecrementCommand { get; private set; }
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }


        public SomeTemplateListViewModel()
        {
            IncrementCommand = new RelayCommand(() => Increment(), () => true);
            DecrementCommand = new RelayCommand(() => Decrement(), () => true);
            AddCommand = new RelayCommand(() => Add(), () => true);
            DeleteCommand = new RelayCommand(() => Delete(), () => true);
        }

        private void Delete()
        {
            List.Remove(SelectedItem);
        }

        private void Decrement()
        {
            if (SelectedItem != null)
            {
                SelectedItem.DecreaseManualProgressCommand.Execute(null);
            }
        }

        private void Increment()
        {
            if (SelectedItem != null)
            {
                SelectedItem.IncreaseManualProgressCommand.Execute(null);
            }
        }

        private void Add()
        {
            var item = new SomeTemplateViewModel();
            List.Add(item);
            SelectedItem = item;
        }
    }
}
