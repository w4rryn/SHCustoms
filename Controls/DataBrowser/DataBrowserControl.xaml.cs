using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SHCustoms.Controls.DataBrowser
{
    /// <summary>
    /// Interaction logic for DataBrowser.xaml
    /// </summary>
    public partial class DataBrowserControl : UserControl
    {
        private ViewBase view;

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register("ItemSource",
                                                                                                   typeof(IEnumerable<object>),
                                                                                                   typeof(DataBrowserControl),
                                                                                                   new PropertyMetadata(new ObservableCollection<object>()));

        public DataBrowserControl()
        {
            InitializeComponent();
        }

        public IEnumerable<object> ItemSource
        {
            get => (IEnumerable<object>)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }

        public ViewBase View
        {
            get => view; set
            {
                view = value;
                list.View = view;
            }
        }
    }
}