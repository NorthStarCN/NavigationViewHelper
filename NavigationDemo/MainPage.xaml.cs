using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using NavigationDemo.Controls.NavigationView;
using System.Linq;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace NavigationDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        RootFrameNavigationHelper navigationHelper;
        private object lastSelectedItem = null;
        private bool isLevel1PageNavigation = false;      //是否为一级页面的导航

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
            RootFrame.Navigating += RootFrame_Navigating;
            RootFrame.Navigated += RootFrame_Navigated;
        }

        private async void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string pageName = string.Empty;
            if (args.IsSettingsInvoked)
            {
                pageName = "Settings";
                SettingsDialog dialog = new SettingsDialog();
                dialog.Closed += SettingsDialogClosed;
                await dialog.ShowAsync();
            }
            else
            {
                isLevel1PageNavigation = true;
                var invokedItem = args.InvokedItemContainer;
                pageName = invokedItem.Content as string;
                RootFrame.Navigate(typeof(ItemPage), pageName);
            }
        }

        private void NavigationViewControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (LocalSettings.Settings.AutoNavigateToHome)
            {
                Category first = Categories.FirstOrDefault(v => v is Category) as Category;
                if (first != null)
                {
                    NavigationViewControl.SelectedItem = first;
                    RootFrame.Navigate(typeof(ItemPage), first.Name);
                }
            }
        }

        /// <summary>
        /// Recover navigation view selected item after settings dialog closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsDialogClosed(ContentDialog sender, ContentDialogClosedEventArgs e)
        {
            if (lastSelectedItem != null)
            {
                NavigationViewControl.SelectedItem = lastSelectedItem;
            }
        }

        private void RootFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            //一级页面不允许两次导航到相同页面
            if(isLevel1PageNavigation 
                && e.NavigationMode == NavigationMode.New 
                && lastSelectedItem == NavigationViewControl.SelectedItem)
            {
                e.Cancel = true;
            }
        }

        private void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {
            lastSelectedItem = NavigationViewControl.SelectedItem;
            isLevel1PageNavigation = false;
        }
    }
}
