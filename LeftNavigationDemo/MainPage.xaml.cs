using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using NavigationDemo.Controls.NavigationView;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace NavigationDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        RootFrameNavigationHelper navigationHelper;

        private List<CategoryBase> Categories = new List<CategoryBase>()
        {
            new Category() { Name = "Home", Glyph = Symbol.Home, Tooltip = "Home" },
            new Category() { Name = "Friends", Glyph = Symbol.People, Tooltip = "Friends" },
            new Separator(),
            new Category() { Name = "Shop", Glyph = Symbol.Shop, Tooltip = "Shop" },
            new Category() { Name = "Favorite", Glyph = Symbol.Favorite, Tooltip = "Favorite" },
            new Header() { Name = "Files" },
            new Category() { Name = "Documents", Glyph = Symbol.Document, Tooltip = "Documents" },
            new Category() { Name = "Picture", Glyph = Symbol.Pictures, Tooltip = "Pictures" },
            new Category() { Name = "Music", Glyph = Symbol.MusicInfo, Tooltip = "Music" }
        };

        public MainPage()
        {
            this.InitializeComponent();
            navigationHelper = new RootFrameNavigationHelper(RootFrame, NavigationViewControl);
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string pageName = string.Empty;
            if (args.IsSettingsInvoked)
            {
                pageName = "Settings";
            }
            else
            {
                var invokedItem = args.InvokedItemContainer;
                pageName = invokedItem.Content as string;
            }
            RootFrame.Navigate(typeof(ItemPage), pageName);
        }
    }
}
