﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniTC.View
{
    /// <summary>
    /// Logika interakcji dla klasy PanelTC.xaml
    /// </summary>
    public partial class PanelTC : UserControl
    {
        public PanelTC()
        {
            InitializeComponent();
        }

        // DRIVES_COMBOBOX
        //Properties
        public static readonly DependencyProperty ComboboxSourceProperty =
            DependencyProperty.Register(
                nameof(ItmSource), 
                typeof(object), 
                typeof(PanelTC), 
                new PropertyMetadata(null));

        public object ItmSource
        {
            get { return (object)GetValue(ComboboxSourceProperty); }
            set { SetValue(ComboboxSourceProperty, value); }
        }

        public static readonly DependencyProperty ComboboxSelectionProperty =
            DependencyProperty.Register(
                nameof(ItmSelection),
                typeof(object),
                typeof(PanelTC),
                new PropertyMetadata(null));

        public object ItmSelection
        {
            get { return (object)GetValue(ComboboxSelectionProperty); }
            set { SetValue(ComboboxSelectionProperty, value); }
        }

        //Events
        public static readonly RoutedEvent DrivesCBEvent =
        EventManager.RegisterRoutedEvent(nameof(DrivesCB),
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(PanelTC));

        public event RoutedEventHandler DrivesCB
        {
            add { AddHandler(DrivesCBEvent, value); }
            remove { RemoveHandler(DrivesCBEvent, value); }
        }

        //Raising events
        void RaiseDrive_combobox_DropDownOpened()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(PanelTC.DrivesCBEvent);
            RaiseEvent(newEventArgs);
        }

        private void Drive_combobox_DropDownOpened(object sender, EventArgs e)
        {
            RaiseDrive_combobox_DropDownOpened();
        }
    }
}
