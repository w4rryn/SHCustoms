using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SHCustoms.Controls.DataBrowser
{
    public class FilterGridViewColumn : GridViewColumn
    {
        //        <DataTemplate DataType = "{x:Type GridViewColumn}" >
        //    < StackPanel Height="80">
        //        <TextBlock Text = "{Binding}" Margin="20,4" />
        //        <TextBox Text = "" Margin="4,2" />
        //    </StackPanel>
        //</DataTemplate>
        public FilterGridViewColumn()
        {
            DataTemplate template = new DataTemplate();
        }
    }
}