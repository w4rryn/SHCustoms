using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        // Using a DependencyProperty as the backing store for OnMouseDoubleClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OnMouseDoubleClickCommandProperty = DependencyProperty.Register("OnMouseDoubleClickCommand",
                                                                                                                  typeof(ICommand),
                                                                                                                  typeof(DataBrowserControl));

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem",
                                                                                                     typeof(object),
                                                                                                     typeof(DataBrowserControl));

        public DataBrowserControl()
        {
            InitializeComponent();
        }

        public IEnumerable<object> ItemSource
        {
            get => (IEnumerable<object>)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }

        public ICommand OnMouseDoubleClickCommand
        {
            get => (ICommand)GetValue(OnMouseDoubleClickCommandProperty);
            set => SetValue(OnMouseDoubleClickCommandProperty, value);
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public ViewBase View
        {
            get => view; set
            {
                view = value;
                list.View = view;
            }
        }

        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedItem == null)
            {
                return;
            }
            if (OnMouseDoubleClickCommand.CanExecute(SelectedItem))
            {
                OnMouseDoubleClickCommand.Execute(SelectedItem);
            }
        }

        private void FilterTextBoxKeyUp(object sender, KeyEventArgs e)
        {
        }
    }
}