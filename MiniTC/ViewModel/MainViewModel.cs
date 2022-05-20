using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniTC.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MainViewModel()
        {
            PanelLewy = new PanelViewModel();
            PanelPrawy = new PanelViewModel();
        }

        public PanelViewModel PanelLewy { get; set; }
        public PanelViewModel PanelPrawy { get; set; }

        private ICommand copyClick;

        public ICommand CopyClick => copyClick ?? (copyClick = new RelayCommand(
            o =>
            {
                string file = PanelLewy.SelectedFolder;
                string sourceFileName = PanelLewy.FullPath + file;
                string targetFileName = PanelPrawy.FullPath + file;
                try
                {
                    File.Copy(sourceFileName, targetFileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Taki plik już istnieje");
                }
                
                PanelPrawy.FullPath = PanelPrawy.FullPath;
            }
            ,
            o => PanelLewy.SelectedFolder!=null && PanelPrawy.FullPath!=null));
    }
}