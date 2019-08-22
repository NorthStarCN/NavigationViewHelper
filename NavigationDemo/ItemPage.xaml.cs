using System;
using System.Collections.Generic;
using System.Text;
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

        private List<VirtualGridViewItem> GenerateVirtualGridViewItems()
        {
            int count = new Random().Next(1, 16);
            List<VirtualGridViewItem> items = new List<VirtualGridViewItem>();
            Color[] colors = ColorHelper.RandomColors(count);
            for (int index = 0; index < count; index++)
            {
                VirtualGridViewItem item = new VirtualGridViewItem()
                {
                    Name = $"Rectangle {index + 1}",
                    Color = new SolidColorBrush(colors[index])
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

    public static class ColorHelper
    {
        public static Color[] RandomColors(int count)
        {
            Random random = new Random();
            Color[] colors = new Color[count];
            for(int index = 0; index < count; index++)
            {
                byte colorA = (byte)(random.Next(0, 10000) % 256);
                byte colorR = (byte)(random.Next(0, 10000) % 256);
                byte colorG = (byte)(random.Next(0, 10000) % 256);
                byte colorB = (byte)(random.Next(0, 10000) % 256);
                Color color = Color.FromArgb(colorA, colorR, colorG, colorB);
                colors[index] = color;
            }
            return colors;
        }
    }
}
