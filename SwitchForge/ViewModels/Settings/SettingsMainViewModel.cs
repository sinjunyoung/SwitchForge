using SwitchForge.Core.ViewModels;

namespace SwitchForge.ViewModels.Settings;

public class SettingsMainViewModel : MultiToolTabViewModel
{
    public GeneralSettingsMainViewModel General { get; } = new();

    public CompressSettingsMainViewModel Compress { get; } = new();

    public KeyFileSettingsMainViewModel KeyFiles { get; } = new();

    public SettingsMainViewModel()
    {
        Tools.Add(General);
        Tools.Add(Compress);
        Tools.Add(KeyFiles);

        InitializeMultiTools();
    }
}