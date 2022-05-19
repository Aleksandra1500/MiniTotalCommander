using MiniTC.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniTC.ViewModel
{
    class PanelViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public PanelViewModel()
        {
            Drives = new ObservableCollection<string>();
            X = new ObservableCollection<string>();
        }

        //string[] drives = Directory.GetLogicalDrives();
        private ICommand combobox_click;

        public ICommand ComboboxClick => combobox_click ?? (combobox_click = new RelayCommand
                                                                        (
                                                                           o =>
                                                                           {
                                                                               string[] x = new string[5];
                                                                               for(int i=0; i < 5; i++)
                                                                               {
                                                                                   x[i] = i.ToString();
                                                                               }
                                                                               
                                                                               foreach (var item in x)
                                                                                {
                                                                                    X.Add(item);
                                                                                }}

                                                                        , null));

        public ObservableCollection<string> Drives { get; set; }
        public ObservableCollection<string> X { get; set; }
    }
}
