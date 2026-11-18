using Common.Localization;
using Common.Services;
using SwitchForge.Core;
using SwitchForge.Core.Models;
using SwitchForge.Core.UI.Helpers;
using SwitchForge.Core.ViewModels;
using SwitchForge.ViewModels.Settings;
using SwitchForge.ViewModels.Switch;
using SwitchForge.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shell;

namespace SwitchForge.ViewModels;

public class MainViewModel : ToolTabViewModel
{
    public static ImageSource? AppIcon
    {
        get
        {
            try
            {
                var exePath = Process.GetCurrentProcess().MainModule?.FileName;

                if (string.IsNullOrEmpty(exePath))
                    return null;

                using var icon = Icon.ExtractAssociatedIcon(exePath);

                if (icon == null)
                    return null;

                return Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch
            {
                return null;
            }
        }
    }

    public static double LogBoxHeight { get => AppConfig.Instance.Common.LogBoxHeight; set { AppConfig.Instance.Common.LogBoxHeight = value; } }

    public SwitchMainViewModel SwitchMainVM { get; } = new();

    public SettingsMainViewModel Settings { get; } = new();

    public int SelectedTabIndex
    {
        get;
        set
        {
            SetProperty(ref field, value);
            OnPropertyChanged(nameof(ActiveLogEntries));
            OnPropertyChanged(nameof(HasHelpKey));

            var selected = Tools[field];

            if (selected == SwitchMainVM)
                SwitchMainVM?.RefreshKeysStatus();
        }
    }

    public ToolTabViewModel SelectedViewModel
    {
        get => Tools[SelectedTabIndex];
        set
        {
            var index = Tools.IndexOf(value);

            if (index != -1)
                SelectedTabIndex = index;
        }
    }

    public ObservableCollection<LogEntry> ActiveLogEntries => SwitchMainVM.LogEntries;

    public static string AppVersion => $"{AppDomain.CurrentDomain.FriendlyName} - Ver {Utils.ToAppVersionString()}";
        
    public double TaskbarProgressValue { get; set => SetProperty(ref field, value); }

    public TaskbarItemProgressState TaskbarProgressState { get; set => SetProperty(ref field, value); } = TaskbarItemProgressState.None;

    public override bool HasHelpKey => SelectedViewModel?.HasHelpKey ?? false;

    public override string? HelpPageKey => SelectedViewModel?.HelpPageKey;

    internal static MainViewModel? Current { get; set; }

    public MainViewModel()
    {
        Current = this;


        Tools.Add(SwitchMainVM);
        Tools.Add(Settings);

        foreach (var tool in Tools)
        {
            RegisterChild(tool);

            tool.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(HasHelpKey))
                    OnPropertyChanged(nameof(HasHelpKey));
            };
        }
    }

    public static void SaveConfig() => AppConfig.Instance.Save();

    public bool IsAnyChildLocked()
    {
        if (Tools.Any(vm => vm.IsLocked))
            return true;


        foreach (var child in Tools)
        {
            if (child.Tools != null && child.Tools.Any(child => child.IsLocked))
                return true;
        }

        return false;
    }

    public static void SetTaskbarProgress(double value, TaskbarItemProgressState state = TaskbarItemProgressState.Normal)
    {
        if (Current == null) return;

        var dispatcher = Application.Current?.Dispatcher;
        if (dispatcher != null && !dispatcher.CheckAccess())
        {
            dispatcher.Invoke(() => SetTaskbarProgress(value, state));
            return;
        }

        if (value <= 0 || state == TaskbarItemProgressState.None)
        {
            Current.TaskbarProgressState = TaskbarItemProgressState.None;
            Current.TaskbarProgressValue = 0;
        }
        else
        {
            Current.TaskbarProgressState = state;
            Current.TaskbarProgressValue = value / 100.0;

            if (state == TaskbarItemProgressState.Error)
                FlashTaskbar.FlashTaskbarIcon(Application.Current?.MainWindow!);
        }
    }

    public override void ClearLog() => ActiveLogEntries.Clear();
}