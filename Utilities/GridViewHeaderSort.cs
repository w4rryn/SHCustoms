using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SHCustoms.Utilities
{
    //Sauce: https://thomaslevesque.com/2009/03/27/wpf-automatically-sort-a-gridview-when-a-column-header-is-clicked/
    public class GridViewHeaderSort
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command",
                                                                                                        typeof(ICommand),
                                                                                                        typeof(GridViewHeaderSort),
                                                                                                        new UIPropertyMetadata(null, CommandPropertyChangedCallback));

        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.RegisterAttached("PropertyName",
                                                                                                             typeof(string),
                                                                                                             typeof(GridViewHeaderSort),
                                                                                                             new UIPropertyMetadata(null));

        public static DependencyProperty AutoSortProperty { get; } = DependencyProperty.RegisterAttached("AutoSort",
                                                                                                         typeof(bool),
                                                                                                         typeof(GridViewHeaderSort),
                                                                                                         new UIPropertyMetadata(false, AutoSortPropertyChangedCallback));

        private static void AddHeaderClickEventHandlerToListView(ItemsControl listView)
        {
            listView.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
        }

        private static void AutoSortPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListView listView)
            {
                if (HasCommand(listView))
                {
                    AutoSortPropertyRemoveOrAddHeaderClickEventHandler(e, listView);
                }
            }
        }

        private static void AutoSortPropertyRemoveOrAddHeaderClickEventHandler(DependencyPropertyChangedEventArgs e, ListView listView)
        {
            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;
            if (IsInverse(oldValue, newValue))
            {
                listView.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
            }
            if (IsInverse(newValue, oldValue))
            {
                listView.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
            }
        }

        private static bool CanAddColumnHeaderEventHandler(in DependencyPropertyChangedEventArgs e)
        {
            return e.OldValue == null && e.NewValue != null;
        }

        private static bool CanRemoveColumnHeaderClickEventHandler(in DependencyPropertyChangedEventArgs e)
        {
            return e.OldValue != null && e.NewValue == null;
        }

        private static void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GridViewColumnHeader headerClicked)
            {
                var propertyName = GetPropertyName(headerClicked.Column);
                if (!string.IsNullOrEmpty(propertyName))
                {
                    ListView listView = GetAncestor<ListView>(headerClicked);
                    if (listView != null)
                    {
                        ICommand command = GetCommandFromObject(listView);
                        if (command != null)
                        {
                            if (command.CanExecute(propertyName))
                            {
                                command.Execute(propertyName);
                            }
                        }
                        else if (IsAutoSort(listView))
                        {
                            ApplySort(listView.Items, propertyName);
                        }
                    }
                }
            }
        }

        private static void CommandPropertyChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o is ItemsControl listView)
            {
                if (!IsAutoSort(listView))
                {
                    RemoveOrAddHeaderClickEventHandler(e, listView);
                }
            }
        }

        private static bool HasCommand(ListView listView)
        {
            return GetCommandFromObject(listView) == null;
        }

        private static bool IsInverse(bool v1, bool inverseOf)
        {
            return v1 && !inverseOf;
        }

        private static void RemoveHeaderClickEventHandlerFromListView(ItemsControl listView)
        {
            listView.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
        }

        private static void RemoveOrAddHeaderClickEventHandler(DependencyPropertyChangedEventArgs e, ItemsControl listView)
        {
            if (CanRemoveColumnHeaderClickEventHandler(in e))
            {
                RemoveHeaderClickEventHandlerFromListView(listView);
            }
            if (CanAddColumnHeaderEventHandler(in e))
            {
                AddHeaderClickEventHandlerToListView(listView);
            }
        }

        public static void ApplySort(ICollectionView view, string propertyName)
        {
            ListSortDirection direction = ListSortDirection.Ascending;
            if (view.SortDescriptions.Count > 0)
            {
                SortDescription currentSort = view.SortDescriptions[0];
                if (currentSort.PropertyName == propertyName)
                {
                    if (currentSort.Direction == ListSortDirection.Ascending)
                        direction = ListSortDirection.Descending;
                    else
                        direction = ListSortDirection.Ascending;
                }
                view.SortDescriptions.Clear();
            }
            if (!string.IsNullOrEmpty(propertyName))
            {
                view.SortDescriptions.Add(new SortDescription(propertyName, direction));
            }
        }

        public static T GetAncestor<T>(DependencyObject reference) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(reference);
            while (!(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent != null)
            {
                return (T)parent;
            }
            else
            {
                return null;
            }
        }

        public static ICommand GetCommandFromObject(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static string GetPropertyName(DependencyObject obj)
        {
            return (string)obj.GetValue(PropertyNameProperty);
        }

        public static bool IsAutoSort(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoSortProperty);
        }

        public static void SetAutoSort(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoSortProperty, value);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        public static void SetPropertyName(DependencyObject obj, string value)
        {
            obj.SetValue(PropertyNameProperty, value);
        }
    }
}