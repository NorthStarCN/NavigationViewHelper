using Windows.Storage;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“内容对话框”项模板

namespace NavigationDemo
{
    public sealed partial class SettingsDialog : ContentDialog
    {
        private LocalSettings settings;

        private string[] Themes = new string[]
        {
            "Auto","Dark","Light"
        };

        public SettingsDialog()
        {
            this.InitializeComponent();
            settings = new LocalSettings();
        }
    }

    public class LocalSettings
    {
        public static LocalSettings Settings = new LocalSettings();

        private ApplicationDataContainer localSettings;
        public LocalSettings()
        {
            localSettings = ApplicationData.Current.LocalSettings;
        }

        public bool AutoNavigateToHome
        {
            get => ReadSettingItemValue(nameof(AutoNavigateToHome), false);
            set => SaveSettingItem(nameof(AutoNavigateToHome), value);
        }

        public string RequestedTheme
        {
            get => ReadSettingItemValue(nameof(RequestedTheme), "Auto");
            set => SaveSettingItem(nameof(RequestedTheme), value);
        }

        private void SaveSettingItem(string key, object value)
        {
            localSettings.Values[key] = value;
        }

        private T ReadSettingItemValue<T>(string key, T defaultValue)
        {
            if (localSettings.Values.ContainsKey(key))
            {
                return (T)localSettings.Values[key];
            }
            if (defaultValue != null)
            {
                return defaultValue;
            }
            return default(T);
        }
    }
}
