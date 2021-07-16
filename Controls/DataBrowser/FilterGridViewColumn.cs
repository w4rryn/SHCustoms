using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SHCustoms.Controls.DataBrowser
{
    public class FilterGridViewColumn : GridViewColumn
    {
        public FilterGridViewColumn()
        {
            DataTemplate header = new();
            FrameworkElementFactory stackPanelFactory = CreateHeaderStackPanelFactory();
            FrameworkElementFactory title = CreateHeaderTextFactory();
            FrameworkElementFactory filterBox = CreateHeaderFilterBox();
            stackPanelFactory.AppendChild(title);
            stackPanelFactory.AppendChild(filterBox);
            header.VisualTree = stackPanelFactory;
            HeaderTemplate = header;
        }

        public ICommand SortCommand
        {
            get => (ICommand)GetValue(SortCommandProperty);
            set => SetValue(SortCommandProperty, value);
        }

        public static readonly DependencyProperty SortCommandProperty = DependencyProperty.Register("SortCommand", typeof(ICommand), typeof(FilterGridViewColumn));

        public object PropertyName { get; set; }

        private FrameworkElementFactory CreateHeaderFilterBox()
        {
            FrameworkElementFactory box = new(typeof(TextBox));
            box.AddHandler(UIElement.KeyUpEvent, new KeyEventHandler(OnKeyUp));
            return box;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            var parameters = new SortParameters(sender, e, PropertyName);
            if (SortCommand != null && SortCommand.CanExecute(parameters))
            {
                SortCommand.Execute(parameters);
            }
        }

        private FrameworkElementFactory CreateHeaderTextFactory()
        {
            FrameworkElementFactory text = new(typeof(TextBlock));
            text.SetBinding(TextBlock.TextProperty, new Binding());
            return text;
        }

        private FrameworkElementFactory CreateHeaderStackPanelFactory()
        {
            FrameworkElementFactory stack = new(typeof(StackPanel));
            stack.SetValue(StackPanel.OrientationProperty, Orientation.Vertical);
            return stack;
        }
    }
}