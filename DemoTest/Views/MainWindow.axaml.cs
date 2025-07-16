using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using sbavalonia.symbols;
using System;
using System.Collections;

namespace DemoTest.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            datagrid.PropertyChanged += Datagrid_PropertyChanged;
        }

        private void Datagrid_PropertyChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
        {
            string propname = e.Property.Name;

            if (propname.Equals(nameof(datagrid.SelectedIndex)))
            {
                //var list = datagrid.ItemsSource as IList;
                //if (list is not null)
                //{
                //    if (list[datagrid.SelectedIndex] is not null)
                //    {
                //        datagrid.ScrollIntoView(list[datagrid.SelectedIndex], null);
                //    }
                //}
            }
            else if (propname.Equals(nameof(datagrid.SelectedItem)))
            {
                Dispatcher.UIThread.Invoke(new Action(() => {
                    datagrid.ScrollIntoView(datagrid.SelectedItem, null);
                }));
            }
        }
    }
}