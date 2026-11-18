using Common.Models;
using SwitchForge.Core;
using SwitchForge.Core.ViewModels;

namespace SwitchForge.ViewModels.Settings;

public class CompressSettingsMainViewModel : ToolTabViewModel
{
    public double SwitchCompressLevel
    {
        get => AppConfig.Instance.Switch.CompressLevel;
        set { AppConfig.Instance.Switch.CompressLevel = (int)value; OnPropertyChanged(); }
    }

    public SwitchCompressionMode SwitchCompressionMode
    {
        get => AppConfig.Instance.Switch.CompressionMode;
        set
        {
            if (AppConfig.Instance.Switch.CompressionMode == value)
                return;

            AppConfig.Instance.Switch.CompressionMode = value;
            OnPropertyChanged();
        }
    }

    public CompressSettingsMainViewModel()
    {
        AppConfig.Instance.Switch.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(SwitchConfig.CompressionMode))
                OnPropertyChanged(nameof(SwitchCompressionMode));
        };
    }
}