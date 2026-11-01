using Avalonia.Controls;
using Res = NSW.Core.Properties.Resources;

namespace NSW.M2.Avalonia.Views;

public partial class MainWindow : Window
{
    public static string AppVersion
    {
        get
        {
            string version = Common.Utils.ToAppVersionString();
            return $"{Res.Main_Title} - Ver {version}";
        }
    }

    public MainWindow()
    {
        InitializeComponent();
        this.Title = AppVersion;
    }
}