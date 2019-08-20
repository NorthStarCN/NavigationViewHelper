using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace NavigationDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ItemPage : Page
    {
        private string PageName;
        private List<VirtualGridViewItem> Items;

        public ItemPage()
        {
            this.InitializeComponent();
            this.Items = GenerateVirtualGridViewItems();
            //this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PageName = e.Parameter as string;
            //base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.Items.Clear();
            //base.OnNavigatingFrom(e);
        }

        private Color[] colors = new Color[]
        {
            Colors.Chocolate,
            Colors.DarkSlateBlue,
            Colors.DeepPink,
            Colors.Purple
        };

        private List<VirtualGridViewItem> GenerateVirtualGridViewItems()
        {
            int count = new Random().Next(16);
            List<VirtualGridViewItem> items = new List<VirtualGridViewItem>();
            for (int index = 0; index < count; index++)
            {
                VirtualGridViewItem item = new VirtualGridViewItem()
                {
                    Name = $"Rectangle {index + 1}",
                    Color = new SolidColorBrush(colors[index % (colors.Length)])
                };
                items.Add(item);
            }
            return items;
        }
    }

    class VirtualGridViewItem
    {
        public string Name { get; set; }
        public SolidColorBrush Color { get; set; }
    }
}
