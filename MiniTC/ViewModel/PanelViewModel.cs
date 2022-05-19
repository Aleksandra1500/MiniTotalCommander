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
        }

        private ICommand combobox_click;

        public ICommand ComboboxClick => combobox_click ?? (combobox_click = new RelayCommand
                                                                        (
                                                                           o =>
                                                                           {
                                                                               Drives.Clear();
                                                                               string[] drives = Directory.GetLogicalDrives();

                                                                               foreach (var item in drives)
                                                                                {
                                                                                    Drives.Add(item);
                                                                                }
                                                                           }

                                                                        , null));

        public ObservableCollection<string> Drives { get; set; }

        private string _sdrive;
        public string SDrive
        {
            get => _sdrive;
            set
            {
                _sdrive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SDrive)));
            }
        }
    }
}
