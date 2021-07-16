using System.Windows.Input;

namespace SHCustoms.Controls.DataBrowser
{
    public class SortParameters
    {
        public object Sender { get; }
        public KeyEventArgs KeyEventArgs { get; }
        public object Property { get; }

        public SortParameters(object sender, KeyEventArgs e, object propertyName)
        {
            Sender = sender;
            KeyEventArgs = e;
            Property = propertyName;
        }
    }
}