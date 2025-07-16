using Avalonia.Collections;
using Avalonia.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbavalonia.controls
{
    internal class DataGridEx : DataGrid
    {
        public DataGridEx()
            : base()
        {
            PropertyChanged += DataGridEx_PropertyChanged;
        }

        private void DataGridEx_PropertyChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
        {
            
        }

        public void ScrollToIndex(int index)
        {

        }

        public void ScrollToEnd()
        {
            //var e = ItemsSource.GetEnumerator();
            //while (e.MoveNext())
            //{
            //    ScrollIntoView(e.Current, null);
            //}
            
        }
    }
}
