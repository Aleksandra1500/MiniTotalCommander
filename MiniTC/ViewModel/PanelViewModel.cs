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
            ListOfFolders = new ObservableCollection<string>();
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
                FullPath = SDrive;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SDrive)));
            }
        }

        private string _fullpath;
        public string FullPath
        {
            get => _fullpath;
            set
            {
                ListOfFolders.Clear();
                _fullpath = value;
                if(FullPath != null)
                {
                    try
                    {
                        string[] FoldersList = Directory.GetDirectories(FullPath);
                        string[] FilesList = Directory.GetFiles(FullPath);
                        string temp = "..";
                        if (FullPath.Length > 3)
                        {
                            ListOfFolders.Add(temp);
                        }

                        foreach (string folder in FoldersList)
                        {
                            temp = Path.GetFileName(folder);
                            temp = "<D>" + temp;
                            ListOfFolders.Add(temp);
                        }
                        foreach (string file in FilesList)
                        {
                            temp = Path.GetFileName(file);
                            ListOfFolders.Add(temp);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Nie można tam wejść");
                        FullPath = Path.GetDirectoryName(Path.GetDirectoryName(FullPath));
                    }
                }
                
                //SelectedFolder = FullPath;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FullPath)));
            }
        }

        public ObservableCollection<string> ListOfFolders { get; set; }

        private string _selectedFolder;

        public string SelectedFolder
        {
            get => _selectedFolder;
            set
            {
                _selectedFolder = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedFolder)));
            }
        }

        private ICommand myListBoxDoubleClick;
        public ICommand MyListBoxDoubleClick => myListBoxDoubleClick ?? (myListBoxDoubleClick = new RelayCommand( 
            o => {
                if (SelectedFolder.Equals(".."))
                {
                    FullPath = Path.GetDirectoryName(Path.GetDirectoryName(FullPath));
                    if (!FullPath.EndsWith("\\")) 
                    { 
                        FullPath += "\\";
                    }
                }
                else if (SelectedFolder.StartsWith("<D>"))
                {
                    FullPath += SelectedFolder.Remove(0, 3) + "\\";
                }
                SelectedFolder = null;
            }
            ,null));
    }
}
