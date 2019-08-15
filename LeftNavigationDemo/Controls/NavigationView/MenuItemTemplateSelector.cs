using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace NavigationDemo.Controls.NavigationView
{
    [ContentProperty(Name = "ItemTemplate")]
    public class MenuItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ItemTemplate { get; set; }
        public DataTemplate SeparatorTemplate = (DataTemplate)XamlReader.Load(
            @"<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
                <NavigationViewItemSeparator />
              </DataTemplate>");
        public DataTemplate HeaderTemplate = (DataTemplate)XamlReader.Load(
            @"<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
                <NavigationViewItemHeader Content='{Binding Name}'/>
              </DataTemplate>");

        protected override DataTemplate SelectTemplateCore(object item)
        {
            return item is Separator ? SeparatorTemplate :
                item is Header ? HeaderTemplate : ItemTemplate;
        }
    }
}
