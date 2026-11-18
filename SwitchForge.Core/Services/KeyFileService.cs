using NSW.Core;

namespace SwitchForge.Core.Services;

public static class KeyFileService
{
    public static void Apply(KeyFilesConfig config)
    {
        KeySetProvider.Instance.SetCustomKeysPath(config.SwitchProdKeysPath);
    }
}
