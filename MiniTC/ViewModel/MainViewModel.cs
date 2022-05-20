using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                if (PanelPrawy.SelectedFolder!=null)
                {
                    string Pfile = PanelPrawy.SelectedFolder;
                    string PsourceFileName = PanelPrawy.FullPath + Pfile;
                    string LtargetFileName = PanelLewy.FullPath + Pfile;
                    try
                    {
                        File.Copy(PsourceFileName, LtargetFileName);
                    }
                    catch
                    {
                        Console.WriteLine("Nie udało się skopiować pliku");
                        MessageBox.Show("Nie udało się skopiować pliku");
                    }
                    PanelLewy.FullPath = PanelLewy.FullPath;
                }
                else
                {
                    string Lfile = PanelLewy.SelectedFolder;
                    string LsourceFileName = PanelLewy.FullPath + Lfile;
                    string PtargetFileName = PanelPrawy.FullPath + Lfile;
                    try
                    {
                        File.Copy(LsourceFileName, PtargetFileName);
                    }
                    catch
                    {
                        Console.WriteLine("Nie udało się skopiować pliku");
                        MessageBox.Show("Nie udało się skopiować pliku");
                    }
                    PanelPrawy.FullPath = PanelPrawy.FullPath;
                }
            }
            ,
            o => (PanelLewy.SelectedFolder!=null && PanelPrawy.FullPath!=null && !PanelLewy.SelectedFolder.StartsWith("<D>")) || 
                    (PanelPrawy.SelectedFolder != null && PanelLewy.FullPath != null && !PanelPrawy.SelectedFolder.StartsWith("<D>"))));
    }
}