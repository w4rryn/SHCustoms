using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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



        private static FrameworkElementFactory CreateHeaderFilterBox()
        {
            FrameworkElementFactory box = new(typeof(TextBox));
            return box;
        }

        private static FrameworkElementFactory CreateHeaderTextFactory()
        {
            FrameworkElementFactory text = new(typeof(TextBlock));
            text.SetBinding(TextBlock.TextProperty, new Binding());
            return text;
        }

        private static FrameworkElementFactory CreateHeaderStackPanelFactory()
        {
            FrameworkElementFactory stack = new(typeof(StackPanel));
            stack.SetValue(StackPanel.OrientationProperty, Orientation.Vertical);
            return stack;
        }
    }
}