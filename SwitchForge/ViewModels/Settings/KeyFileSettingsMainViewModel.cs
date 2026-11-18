using SwitchForge.Core;
using SwitchForge.Core.ViewModels;
using SwitchForge.Core.ViewModels.Settings;

namespace SwitchForge.ViewModels.Settings;

public class KeyFileSettingsMainViewModel : ToolTabViewModel
{
    public IReadOnlyList<KeyFilePathItemViewModel> SwitchItems { get; }

    public KeyFileSettingsMainViewModel()
    {
        var config = AppConfig.Instance.KeyFiles;

        SwitchItems = [KeyFileItems.SwitchProdKeys(config)];
    }
}
