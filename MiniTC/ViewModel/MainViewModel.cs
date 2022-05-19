using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.ViewModel
{
    class MainViewModel
    {
        public MainViewModel()
        {
            PanelLewy = new PanelViewModel();
            PanelPrawy = new PanelViewModel();
        }

        public PanelViewModel PanelLewy { get; set; }
        public PanelViewModel PanelPrawy { get; set; }
    }
}
