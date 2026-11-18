using Common.Localization;
using NSW.Core;

namespace SwitchForge.Core.ViewModels.Settings;

public static class KeyFileItems
{
    public static KeyFilePathItemViewModel SwitchProdKeys(KeyFilesConfig config) => new("prod.keys", "prod.keys", $"prod.keys|prod.keys|{Loc.S.FilterAllFiles}|*.*", config, () => config.SwitchProdKeysPath, value => config.SwitchProdKeysPath = value, () => KeySetProvider.Instance.KeySet != null);
}